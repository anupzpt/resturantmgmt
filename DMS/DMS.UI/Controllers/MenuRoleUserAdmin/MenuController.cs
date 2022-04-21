using AutoMapper;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Mvc;
using DMS.UI.Services.General.Interface;
using DMS.DAL.EntityModels;
using DMS.UI.ViewModels;
using DMS.DAL.DatabaseContext;
using DMS.Services.General.Interface;
using DMS.DAL.StaticHelper;
using DMS.DAL;
using DMS.DAL.Helpers;

namespace DMS.UI.Controllers.MenuRoleUser_Admin
{
    [CustomAuthentication]
    public class MenuController : Controller
    {
        private IMenuServices _menuServices;
        private IControllerActionServices _controllerAction;
        private SystemInfoForSession _ActiveSession;
        private MainEntities _db;
        //private IRolePermissionServices _rolePermission;
        //private IMasterSettingServices _masterSetting;

        public MenuController()
        {
        }

        public MenuController(
            MainEntities db,
            IRolePermissionServices rolePermission,
            IMenuServices menuServices,
            IControllerActionServices controllerAction//, 
                                                      /* IMasterSettingServices masterSetting*/)
        {
            _db = db;
            _menuServices = menuServices;
            _controllerAction = controllerAction;
            _ActiveSession = SessionHelper.GetSession();
            //_rolePermission = rolePermission;
            //_masterSetting = masterSetting;
        }

        public ActionResult DisplayMenu()
        {
            try
            {
                TempData["Version"] = DMS.Properties.Settings.Default.SystemVersion;
                //var systemSession = (SystemInfoForSession)Session["SystemSession"];
                List<MenuListVM> model = null;
                var userid = User.Identity.GetUserId();
                if (Session["menuList"] == null)
                {
                    //    List<MenuListVM> menuList = _menuServices.MenuListForDisplay(userid, null);
                    //  Session["menuList"] = menuList;
                    //   Session.Timeout = 1440;
                }
                //if (!systemSession.IsAdmin && !systemSession.IsSuperAdmin && !SystemInfo.IsBeginOFDay)
                //{
                //    TempData["BODMessage"] = "In Your System BOD Has Not Start. Please Start BOD";
                //}
                model = (List<MenuListVM>)Session["menuList"];
                return PartialView("~/Views/Menu/EditorTemplate/DisplayMenuPartialView.cshtml", model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: DymanicMenu
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                var menuList = Mapper.Map<List<MenuListVM>>(await _menuServices.GetAll());
                //var areaList = _db.MenuAreas;
                //foreach(var data in menuList)
                //{
                //    data.DisplayArea = data.Area_id!=null && data.Area_id != 0 ? areaList.FirstOrDefault(x => x.Id == data.Area_id).title : "";
                //}
                return View(menuList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: DymanicMenu/Create
        [HttpGet]
        public async Task<ActionResult> CreateNewMenu()
        {
            try
            {
                var allController = await _controllerAction.GetAllControllerActionList();
                var menu = new MenuListVM();
                menu.IsActive = true;
                ViewBag.MenuType = new SelectList(EnumHelper.GetEnumList<EnumMenuTypes>(), "BindField", "DisplayField");
                //ViewBag.Area = new SelectList(_db.MenuAreas, "id", "title");
                ViewBag.ControllerName = new SelectList(allController.DistinctBy(x => x.ControllerName), "Id", "ControllerName");
                ViewBag.ParentId = new SelectList(_menuServices.GetAllParentMenu(), "Id", "Title");
                return View(menu);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult CreateMenuPartialList(MenuListVM menu)
        {
            if (menu.DropDownName == null)
            {
                var controllerDetails = _controllerAction.GetControllerActionById(menu.ActionId);
                if (controllerDetails == null)
                {
                    return Content("");
                }
                menu.ActionName = controllerDetails.ActionName;
                //menu.IconName = controllerDetails.;
                menu.ControllerName = controllerDetails.ControllerName;
                //menu.ParentId = menu.ParentId;
            }
            return PartialView(menu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNewMenu(MenuListInfo menuList)
        {
            if (menuList.menuCollection != null && menuList.menuCollection.Count() > 0)
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    try
                    {
                        foreach (var item in menuList.menuCollection)
                        {
                            //item.Id = Guid.NewGuid().ToString();
                            if (item.DropDownName != null)
                            {
                                item.Title = item.DropDownName;
                            }
                            if (item.ParentId == 0)
                            {
                                item.ParentId = 0;
                            }
                            var menuData = Mapper.Map<MenuList>(item);
                            menuData.ControllerAction = null;
                            if (item.ActionId == 0)
                            {
                                menuData.ControllerActionId = null;
                            }
                            else
                            {
                                menuData.ControllerActionId = item.ActionId;
                            }

                            _menuServices.AddSync(menuData);
                        }
                        ts.Complete();
                        ts.Dispose();
                        TempData["Message"] = "Process Successfully";
                        return RedirectToAction("Index");
                    }
                    catch (DbEntityValidationException dbex)
                    {
                        foreach (var valEx in dbex.EntityValidationErrors)
                        {
                            foreach (var valEx2 in valEx.ValidationErrors)
                            {
                                Trace.TraceInformation("Property: {0} Error: {1}", valEx2.PropertyName, valEx2.ErrorMessage);
                            }
                        }
                        throw dbex;
                    }
                }
            }
            TempData["Message"] = "Data Not Saved";
            return RedirectToAction("CreateNewMenu");
        }

        //Use For Get All Action Name Filter By Controller Name
        public ActionResult GetAllActionByController(int controllerId)
        {
            try
            {
                var AllActionData = _controllerAction.GetAllActionDataByController(controllerId);
                return Json(AllActionData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: DymanicMenu/Edit/5
        [HttpGet]
        public ActionResult EditMenuWithParent()
        {
            var menuinforEditList = _menuServices.getAllMenuList();
            if (menuinforEditList == null)
            {
                return HttpNotFound();
            }
            return View(menuinforEditList);
        }

        [HttpPost]
        public ActionResult EditMenuWithParent(List<MenuInfoVM> MenuInfoList)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    if (MenuInfoList.Count() > 0)
                    {
                        _menuServices.SaveMenuListWithParent(MenuInfoList);
                        ts.Complete();
                        ts.Dispose();
                        TempData["Message"] = "Process Successfully";
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    TempData["Message"] = "Error Occurred .. Process Not Complete </br>" + ex;
                    ts.Dispose();
                    throw ex;
                }
            }
            return RedirectToAction("EditMenuWithParent");
        }

        public async Task<ActionResult> EditMenuById(int id)
        {

            var menuDetails = _menuServices.GetMenuById(id);
            var allController = await _controllerAction.GetAll();
            var DistinctController = Mapper.Map<List<ControllerActionVM>>(allController.DistinctBy(x => x.ControllerName).ToList());
            int controllerId = 0;
            int parentid = 0;
            if (menuDetails.ParentId != 0 && menuDetails.ControllerAction != null)
            {
                controllerId = DistinctController.Where(x => x.ControllerName == menuDetails.ControllerAction.ControllerName).FirstOrDefault().Id;
                ViewBag.actionlist = allController.Where(x => x.ControllerName == menuDetails.ControllerAction.ControllerName).ToList();
                ViewBag.ActionId = new SelectList(_controllerAction.GetAllActionDataByController(controllerId), "Id", "ActionName", menuDetails.ControllerAction.ActionName);
                parentid = menuDetails.ParentId;
            }
            ViewBag.controllerList = new SelectList(DistinctController.ToList(), "Id", "ControllerName", controllerId);
            ViewBag.MenuType = new SelectList(EnumHelper.GetEnumList<EnumMenuTypes>(), "BindField", "DisplayField", menuDetails.MenuType);
            //ViewBag.Area = new SelectList(_db.MenuAreas, "id", "title", menuDetails.Area_id);
            ViewBag.ParentId = new SelectList(_menuServices.GetAllParentMenu(), "Id", "Title", parentid);
            return View(menuDetails);
        }

        public async Task<ActionResult> Details(int id)
        {

            var menuDetails = _menuServices.GetMenuById(id);
            var allController = await _controllerAction.GetAll();
            var DistinctController = Mapper.Map<List<ControllerActionVM>>(allController.DistinctBy(x => x.ControllerName).ToList());
            int controllerId = 0;
            int parentid = 0;
            if (menuDetails.ParentId != 0 && menuDetails.ControllerAction != null)
            {
                controllerId = DistinctController.Where(x => x.ControllerName == menuDetails.ControllerAction.ControllerName).FirstOrDefault().Id;
                ViewBag.actionlist = allController.Where(x => x.ControllerName == menuDetails.ControllerAction.ControllerName).ToList();
                ViewBag.ActionId = new SelectList(_controllerAction.GetAllActionDataByController(controllerId), "Id", "ActionName", menuDetails.ControllerAction.ActionName);
                parentid = menuDetails.ParentId;
            }
            ViewBag.controllerList = new SelectList(DistinctController.ToList(), "Id", "ControllerName", controllerId);
            ViewBag.ParentId = new SelectList(_menuServices.GetAllParentMenu(), "Id", "Title", parentid);
            return View(menuDetails);
        }


        [HttpPost]
        public ActionResult EditMenuById(MenuListVM menu)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    menu.ControllerAction = null;
                    _menuServices.SaveEditedSingleMenuDetails(menu);
                    ts.Complete();
                    ts.Dispose();
                    TempData["Message"] = "Process Successfully";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["ErrMessage"] = "Error Occurred .. Process Not Complete </br>" + ex;
                    ts.Dispose();
                    throw ex;
                }
            }
        }

        public ActionResult CreateMenu()
        {
            IList<MenuListVM> MenuListVM_List = _menuServices.GetAllMenuListbyEmpType();
            return PartialView(MenuListVM_List);
        }

        public ActionResult SideMenu()
        {
            ViewBag.IsSuperAdmin = _ActiveSession.IsSuperAdmin;
            ViewBag.IsOrgAdmin = _ActiveSession.IsOrgAdmin;
            return PartialView();
        }

    }
}