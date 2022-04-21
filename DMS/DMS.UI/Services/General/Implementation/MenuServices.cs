using DMS.UI.Services.General.Interface;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DMS.DAL.EntityModels;
using DMS.DAL.Repositories.GenericRepo;
using DMS.DAL.StaticHelper;
using Microsoft.Ajax.Utilities;
using DMS.UI.ViewModels;
using DMS.DAL.Repositories.GeneralRepo.Interfaces;
using DMS.DAL.Repositories.GeneralRepo.Implementation;
using DMS.DAL.DatabaseContext;
using DMS.Services.General.Interface;
using DMS.ViewModels;
using System;

namespace DMS.UI.Services.General.Implementation
{
    public class MenuServices : GenericRepo<MenuList, string>, IMenuServices
    {
        //private readonly ICFGenericRepo<MenuRole, string> _menuRoleRepo;
        private readonly IGenericRepo<ControllerAction, string> _controllerAction;
        private readonly ICFGenericRepo<ApplicationUser, string> _roleManagement;
        private readonly IMenuRepo _menuRepo = new MenuRepo();
        private readonly IRolePermissionServices _rolePermission;
        //private readonly IRolePermissionServices _rolePermission;
        //private readonly IEmployeeServices _employee;
        // private MainEntities db = new MainEntities();


        private SystemInfoForSession _ActiveSession;

        public MenuServices(
            GenericRepo<ControllerAction, string> controllerAction,
            CFGenericRepo<ApplicationUser, string> roleManagement,
            //CFGenericRepo<MenuRole, string> menuRoleRepo, 
            IRolePermissionServices rolePermission/*, IMenuServices menuServices*/
                                                  //, IEmployeeServices employee
            )
        {
            _controllerAction = controllerAction;
            _roleManagement = roleManagement;
            // _menuServices = menuServices;
            //_menuRoleRepo = menuRoleRepo;
            _rolePermission = rolePermission;
            _ActiveSession = SessionHelper.GetSession();
            //_employee = employee;
        }

        public async Task<IEnumerable<SelectListItem>> GetControllerList()
        {
            var allController = await _controllerAction.GetAll();
            var firstRow = new SelectListItem() { Value = null, Text = "<--- Select Controller Name --->" };
            List<SelectListItem> ControllerDropDownList = allController.DistinctBy(x => x.ControllerName).Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.ControllerName
            }).ToList();
            ControllerDropDownList.Insert(0, firstRow);
            return ControllerDropDownList;
        }

        public List<MenuInfoVM> getAllMenuList()
        {
            var AllMenu = Mapper.Map<List<MenuListVM>>(GetAllSync());
            var menuList = AllMenu.Select(x => new MenuInfoVM()
            {
                menuListVM = x,
                Id = x.Id,
                DropDownName = x.Title,
                Description = x.Description,
                IsActive = x.IsActive,
                MenuList = new SelectList(AllMenu.Where(y => y.DropDownName != null).ToList(), "Id", "Description", x.ParentId)
            }).ToList();
            return menuList;
        }
        public List<MenuListVM> GetAllParentMenu()
        {
            List<MenuListVM> AllParentMenu = Mapper.Map<List<MenuListVM>>(GetAllSync());
            AllParentMenu = AllParentMenu.Where(x => x.ParentId == 0).ToList();
            return AllParentMenu;
        }

        public void SaveMenuListWithParent(List<MenuInfoVM> _menuinfo)
        {
            var allMenuWithParentsAndOther = _menuinfo.Select(x => new MenuListVM()
            {
                Id = x.menuListVM.Id,
                ParentId = x.menuListVM.ParentId != 0 ? x.menuListVM.ParentId : 0,
                IsActive = x.menuListVM.IsActive
            }).ToList();
            var model = Mapper.Map<List<MenuList>>(allMenuWithParentsAndOther);
            _menuRepo.SaveMenuListWithParent(model);
            ////var systemSession = (SystemInfoForSession)HttpContext.Current.Session["SystemSession"];
            //foreach (var item in model)
            //{
            //    var menuData = GetByIdSync(item.Id);
            //    menuData.ParentId = item.ParentId == null ? "0" : item.ParentId;
            //    menuData.IsActive = item.IsActive;
            //    //menuData.UpdatedBy = systemSession.UserId;
            //    menuData.UpdatedNepaliDate = SystemInfo.NepaliDate;
            //    UpdateSync(item);
            //}
        }

        //public List<MenuListVM> MenuListForDisplay(string userId, string roleId)
        //{
        //    var allMenuListWithRole = new List<MenuListVM>();
        //    //var systemSession = (SystemInfoForSession)HttpContext.Current.Session["SystemSession"];
        //    //if (systemSession.IsSuperAdmin)
        //    //{
        //    //    allMenuListWithRole = Mapper.Map<List<MenuListVM>>(GetSync(x => x.IsActive == true));
        //    //}
        //    //else
        //    //{
        //    if (roleId != null)
        //    {
        //        allMenuListWithRole = GetAllMenuListForRole(roleId);
        //    }
        //    else if (userId != null)
        //    {
        //        allMenuListWithRole = GetAllMenuWithRole(userId);
        //    }
        //    else
        //    {
        //        allMenuListWithRole = Mapper.Map<List<MenuListVM>>(GetSync(x => x.IsActive == true));
        //    }
        //    //}
        //    var ParentMenu = allMenuListWithRole.Where(x => x.ParentId == 0).ToList();
        //    ParentMenu = ParentMenu.Select(x => new MenuListVM()
        //    {
        //        ParentId = x.ParentId,
        //        ActionId = x.ActionId,
        //        Description = x.Description,
        //        Position = x.Position,
        //        Id = x.Id,
        //        DropDownName = x.DropDownName,
        //        IsActive = x.IsActive,
        //        Title = x.Title,
        //        IconName = x.IconName,
        //        RoleAccess = x.RoleAccess,
        //        ControllerAction = x.ControllerAction,
        //        ChildMenuList = ChildMenuList(x.Id, allMenuListWithRole)
        //    }).ToList();
        //    return ParentMenu.OrderBy(x => x.Position).ToList();
        //}

        public List<MenuListVM> ChildMenuList(int menuId, List<MenuListVM> menuList)
        {
            var ChildMenu = menuList.Where(x => x.ParentId == menuId && x.IsActive == true).Select(x => new MenuListVM()
            {
                ParentId = x.ParentId,
                ActionId = x.ActionId,
                Description = x.Description,
                Position = x.Position,
                DropDownName = x.DropDownName,
                Id = x.Id,
                IsActive = x.IsActive,
                Title = x.Title,
                IconName = x.IconName,
                RoleAccess = x.RoleAccess,
                ControllerAction = x.ControllerAction,
                ChildMenuList = ChildMenuList(x.Id, menuList)
            }).ToList();
            return ChildMenu.OrderBy(x => x.Position).ToList();
        }

        //public List<MenuListVM> GetAllMenuWithRole(string userId)
        //{
        //    var users = _roleManagement.GetByIdSync(userId);
        //    List<MenuListVM> menuList = new List<MenuListVM>();
        //    foreach (var item in users.Roles)
        //    {
        //        var menuIdsList = _menuRoleRepo.GetSync(x => x.RoleId == item.RoleId && x.IsActive == true);
        //        foreach (var item1 in menuIdsList)
        //        {
        //            var accessmenus = Mapper.Map<MenuListVM>(item1.MenuList);
        //            accessmenus.RoleAccess = true;
        //            menuList.Add(accessmenus);
        //        }
        //    }
        //    var distinctMenuListVM = menuList.ToList();
        //    return distinctMenuListVM;
        //}

        public void SaveEditedSingleMenuDetails(MenuListVM menu)
        {
            // var getMenuDetails = GetByIdSync(menu.Id);
            var getMenuDetails = _db.MenuLists.Where(x => x.Id == menu.Id).FirstOrDefault();
            if (menu.ActionId == 0)
            {
                getMenuDetails.ControllerAction = null;
                getMenuDetails.ControllerActionId = null;
            }
            else
            {
                getMenuDetails.ControllerActionId = menu.ActionId;
            }
            if (menu.ParentId > 0)
            {
                getMenuDetails.ParentId = getMenuDetails.ParentId;
            }
            else
            {
                getMenuDetails.ParentId = 0;
            }
            getMenuDetails.Title = menu.Title;
            getMenuDetails.Description = menu.Description;
            getMenuDetails.DropDownName = menu.DropDownName;
            getMenuDetails.IsActive = menu.IsActive;
            getMenuDetails.IconName = menu.IconName;
            getMenuDetails.Position = menu.Position;
            getMenuDetails.Area_id = menu.Area_id;
            getMenuDetails.MenuType = menu.MenuType;
            UpdateSync(getMenuDetails);
        }

        ////For AssingMenuListForRole()
        //public List<MenuListVM> GetAllMenuListForRole(string roleId)
        //{
        //    var menuRoleList = _menuRoleRepo.GetSync(x => x.RoleId == roleId);
        //    var AllMenu = Mapper.Map<List<MenuListVM>>(GetSync(x => x.IsActive == true));
        //    var menuList = AllMenu.Select(x => new MenuListVM()
        //    {
        //        Title = x.Title,
        //        IsActive = x.IsActive,
        //        Id = x.Id,
        //        IconName = x.IconName,
        //        ParentId = x.ParentId,
        //        Position = x.Position,
        //        Description = x.Description,
        //        ControllerAction = x.ControllerAction,
        //        DropDownName = x.DropDownName,
        //        RoleAccess = CheckRoleForMenu(x.Id, menuRoleList)
        //    }).ToList();
        //    return menuList.ToList();
        //}

        //private bool CheckRoleForMenu(int menuId, List<MenuRole> menuRole)
        //{
        //    var menuRoleData = menuRole.Where(x => x.MenuId == menuId).FirstOrDefault();
        //    if (menuRoleData != null)
        //    {
        //        return menuRoleData.IsActive;
        //    }
        //    return false;
        //}

        public IList<MenuListVM> Roles(string[] RoleIds)
        {
            if (RoleIds == null) { return new List<MenuListVM>(); }
            IList<MenuList> dbData = GetList().ToList();
            List<MenuListVM> AllMenuItems = Mapper.Map<List<MenuListVM>>(dbData);

            if (_ActiveSession.IsSuperAdmin)
            {
                return AllMenuItems.ToList();
            }
            IList<AspNetRoleMenuItem> MeuList = _rolePermission.GetApplicationUserMenuList().Where(s => RoleIds.Contains(s.RoleId)).ToList();

            List<MenuListVM> SelectedMenuListVMList = AllMenuItems.Where(x => MeuList.Any(y => y.MenuListId == x.Id)).ToList();
            //foreach (var item in RoleIds)
            //{
            //    var Rolelist = MeuList.Where(x => x.RoleId == item);
            //}
            return SelectedMenuListVMList;
        }
        public IList<MenuListVM> GetAllMenuListbyEmpType()
        {
            var Data = (Roles(_ActiveSession.RoleInfoId)).Where(s => s.IsActive == true).ToList();
            if(Data==null)
            {
                return new List<MenuListVM>();
            }
            return Data;//Where(x => x.usr05userId == SessionInfo.EmployeeId).FirstOrDefault(); //await _employee.GetEmployeebyId(SessionInfo.EmployeeId);
        }
        public MenuListVM GetMenuById(int id)
        {
            MenuList MenuList = _db.MenuLists.Where(x => x.Id == id).FirstOrDefault();
            MenuListVM MenuListVM = Mapper.Map<MenuListVM>(MenuList);
            return MenuListVM;
        }


    }
}