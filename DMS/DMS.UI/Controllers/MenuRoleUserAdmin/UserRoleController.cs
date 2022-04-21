using System;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Mvc;
using DMS.UI.Services.General.Interface;
using AutoMapper;
using System.Collections.Generic;
using DMS.UI.ViewModels;

namespace IMS.UI.Controllers.MenuRoleUser_Admin
{
    public class UserRoleController : Controller
    {
        private ApplicationRoleVM _roleVM = new ApplicationRoleVM();
        private IControllerActionServices _controllerAction;
        private IUserRoleServices _userRole;
        private IMenuServices _menuServices;

        public UserRoleController()
        {
        }

        public UserRoleController(
            IControllerActionServices controllerAction, 
            IUserRoleServices userRole, 
            IMenuServices menuServices)
        {
            _controllerAction = controllerAction;
            _userRole = userRole;
            _menuServices = menuServices;
        }

        // GET: UserRole
        [HttpGet]
        public ActionResult Index()
        {
            var allUserRole = Mapper.Map<List<ApplicationRoleVM>>(_userRole.GetAllSync());
            return View(allUserRole);
        }

        public async Task<ActionResult> EditControllerActionRole(string Id, string Name)
        {
            var data = new ApplicationRoleVM();
            _roleVM.Name = Name;
            _roleVM.ControllerActionRoleList = await _controllerAction.GetAllControllerActionRoleForEdit(Id);
            return View(_roleVM);
        }

        //
        // POST: /Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditControllerActionRole(ApplicationRoleVM _ApplicationRoleVM)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    _userRole.SaveEditedRoleWithControllerList(_ApplicationRoleVM);
                    ts.Complete();
                    ts.Dispose();
                    TempData["Message"] = "Process Successfully";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["Message"] = "Error Occurs .. Process Not Complete" + ex;
                    ts.Dispose();
                    throw ex;
                }
            }
        }

        //Role Assign For Multiple Menu
        //public ActionResult EditRoleWithMenu(string Id, string Name)
        //{
        //    var role = new ApplicationRoleVM();
        //    role.Name = Name;
        //    role.Id = Id;
        //    role.menuListVm = _menuServices.MenuListForDisplay(null, Id);
        //    return View(role);
        //}

        ////Role Assign For Multiple Menu save
        //[HttpPost]
        ////[ValidateAntiForgeryToken]
        //public ActionResult EditRoleWithMenuPost(ApplicationRoleVM role)
        //{
        //    bool result = false;
        //    using (TransactionScope ts = new TransactionScope())
        //    {
        //        try
        //        {
        //            _userRole.SaveRoleWithMultipleMenu(role);
        //            ts.Complete();
        //            ts.Dispose();
        //            TempData["Message"] = "Process Successfully";
        //            result = true;
        //        }
        //        catch (Exception ex)
        //        {
        //            TempData["Message"] = "Error Occurs .. Process Not Complete" + ex;
        //            ts.Dispose();
        //            throw ex;
        //        }
        //    }
        //    //return RedirectToAction("Index");
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
    }
}