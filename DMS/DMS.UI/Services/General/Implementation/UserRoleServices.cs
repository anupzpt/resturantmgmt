using DMS.UI.Services.General.Interface;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using DMS.DAL.EntityModels;
using DMS.DAL.Repositories.GenericRepo;
using DMS.DAL.Repositories.GeneralRepo.Interfaces;
using DMS.DAL.StaticHelper;
using DMS.DAL.Repositories.GeneralRepo.Implementation;
using DMS.UI.ViewModels;
using DMS.DAL.DatabaseContext;

namespace DMS.UI.Services.General.Implementation
{
    public class UserRoleServices : CFGenericRepo<ApplicationRole, string>, IUserRoleServices
    {
        private readonly ICFGenericRepo<ControllerAction, string> _controllerAction;
        private readonly ICFGenericRepo<ApplicationUser, string> _applicationUser;
        private readonly IUserRoleRepo _userRole;
        private readonly IApplicationUserRoleRepo _applicationUserRole;


        public UserRoleServices(CFGenericRepo<ControllerAction, string> controllerAction, CFGenericRepo<ApplicationUser, string> applicationUser, UserRoleRepo userRole, IApplicationUserRoleRepo applicationUserRole)
        {
            _controllerAction = controllerAction;
            _applicationUser = applicationUser;
            _userRole = userRole;
            _applicationUserRole = applicationUserRole;
        }

        //public void AddNewRoleWithMultipleMenu(ApplicationRoleVM applicationRoleVm)
        //{
        //    //var systemSession = (SystemInfoForSession)HttpContext.Current.Session["SystemSession"];
        //    var roleDetails = new ApplicationRole();
        //    var selectedMenuList = applicationRoleVm.menuListVm.Where(x => x.RoleAccess == true).ToList();
        //    var menuRoleList = selectedMenuList.Select(x => new MenuRole()
        //    {
        //        Id = Guid.NewGuid().ToString(),
        //        MenuId = x.Id,
        //        IsActive = true,
        //        //UpdatedBy = systemSession.UserId,
        //        CreatedNepaliDate = SystemInfo.EnglishDate,
        //        UpdatedNepaliDate = SystemInfo.NepaliDate,
        //    }).ToList();
        //    roleDetails.MenuRole = menuRoleList;
        //    roleDetails.Name = applicationRoleVm.Name;
        //    Add(roleDetails);
        //}

        public async Task<ApplicationRoleVM> GetAllRoleListFilterWithRoleName(string RoleName)
        {
            var allRole = await GetAll();
            var RoleFilterbyName = allRole.Where(x => x.Name == RoleName).FirstOrDefault();
            ApplicationRoleVM Role = Mapper.Map<ApplicationRoleVM>(RoleFilterbyName);
            var ControllerActionWithRole = RoleFilterbyName.ActionRole.ToList();
            var ControllerActionRoleList = ControllerActionWithRole.Select(x => new ActionRoleVM()
            {
                controllerActionVm = Mapper.Map<ControllerActionVM>(x.ControllerAction),
                RoleId = x.RoleId,
                IsActive = Convert.ToBoolean(x.IsActive)
            }).ToList();

            Role.ControllerActionRoleList = ControllerActionRoleList;
            return Role;
        }

        public ApplicationRoleVM GetDetailsOfRoleWithControllerAction(string id)
        {
            ApplicationRoleVM roleVm = new ApplicationRoleVM();
            roleVm = Mapper.Map<ApplicationRoleVM>(_applicationUserRole.GetRoleNameById(id));
            roleVm.Id = id;
            var AllListOfControllerActionRole = roleVm.ControllerActionRoleList;
            var ControllerActionList = AllListOfControllerActionRole.Select(x => new ActionRoleVM()
            {
                ActionName = x.controllerActionVm.ActionName,
                ControllerName = x.controllerActionVm.ControllerName,
                IsActive = x.IsActive
            }).ToList();
            roleVm.ControllerActionRoleList = ControllerActionList;
            return roleVm;
        }

        public ApplicationUserVM GetUserByUserName(string UserName)
        {
            var model = Mapper.Map<ApplicationUserVM>(_applicationUserRole.GetUserByUserName(UserName));
            return model;
        }

        public ApplicationRoleVM GetUserListWithRole(string roleId)
        {
            var userList = _applicationUserRole.GetAllUserList();
            var userWithRoleAccess = userList.Select(x => new ApplicationUserVM()
            {
                Id = x.Id,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                UserName = Regex.Replace(x.UserName, @"_", " "),
                IsAssignRole = CheckRoleForUser(roleId, x)
            }).ToList();
            var model = new ApplicationRoleVM();
            model.Id = roleId;
            model.applicationUserVm = userWithRoleAccess.ToList();
            return model;
        }

        private bool CheckRoleForUser(string RoleId, ApplicationUser userDetails)
        {
            var SelectedRole = userDetails.Roles.Where(x => x.RoleId == RoleId).FirstOrDefault();
            if (SelectedRole != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<ApplicationUserVM> GetUserWithRoleList(string id)
        {
            var allRoleFromTable = await GetAll();
            var user = await _applicationUser.GetById(id);
            var userDetails = Mapper.Map<ApplicationUserVM>(user);
            var RoleLists = allRoleFromTable.Select(x => new ApplicationRoleVM()
            {
                Id = x.Id,
                Name = x.Name,
                IsChecked = CheckRoleForUser(x.Id, user)
            }).ToList();
            userDetails.RoleList = RoleLists;
            return userDetails;
        }

        public void SaveEditedRoleWithControllerList(ApplicationRoleVM Role)
        {
            var roleWithMultiplerMenu = Role.ControllerActionRoleList.Select(x => new ActionRole()
            {
                ActionId = x.ActionId,
                RoleId = Role.Id,
                IsActive = x.IsActive
            }).ToList();
            _userRole.SaveEditedRoleWithActionList(roleWithMultiplerMenu);
        }

        public void SaveEditedUserWithRoles(ApplicationUserVM userVm)
        {
            var role = userVm.RoleList.Where(x => x.IsChecked == true).ToList();
            userVm.RoleList = role;
            var model = Mapper.Map<ApplicationUser>(userVm);
            _userRole.SaveEditedUserWithRoles(model);
        }

        //public void SaveRoleForMenu(MenuListVM roleWithMenu)
        //{
        //    var menuRoleAccess = roleWithMenu.RolesList.Select(x => new MenuRole()
        //    {
        //        MenuId = roleWithMenu.Id,
        //        RoleId = x.Id,
        //        IsActive = x.IsChecked
        //    }).ToList();
        //    _userRole.SaveRoleForMultipleMenu(menuRoleAccess);
        //}

        //public void SaveRoleWithMultipleMenu(ApplicationRoleVM role)
        //{
        //    var roleWithMultiplerMenu = role.menuListVm.Select(x => new MenuRole()
        //    {
        //        MenuId = x.Id,
        //        RoleId = role.Id,
        //        IsActive = x.RoleAccess
        //    }).ToList();
        //    _userRole.SaveRoleForMultipleMenu(roleWithMultiplerMenu);
        //}

        public void SaveUserWithRole(ApplicationUserVM userVM)
        {
            ////_UserVM.UserLevel = _userLevel.getuserLevelId(Convert.ToString(_UserVM.UserLevelEnum));
            //_UserVM.Role = _UserVM.Role.Where(x => x.IsChecked == true).ToList();
            //var model = Mapper.Map<AspNetUser>(_UserVM);
            //_roleManagement.SaveUserWithRole(model);
        }

        public void UpdateUserListWithRole(ApplicationRoleVM role)
        {
            var RoleAccesUserList = Mapper.Map<List<ApplicationUser>>(role.applicationUserVm.Where(x => x.IsAssignRole == true)).ToList();
            _userRole.UpdateUserListWithRole(RoleAccesUserList, role.Id);
        }

        public ApplicationUserVM GetUserById(string id) => Mapper.Map<ApplicationUserVM>(_applicationUser.GetById(id));

        public List<ApplicationUserVM> GetAllUserList()
        {
            var model = Mapper.Map<List<ApplicationUserVM>>(_applicationUser.GetAllSync());
            return model;
        }
        public bool RoleExists(string RoleName)
        {
            return _userRole.RoleExists(RoleName);
        }
        public void AddRole(string RoleName)
        {
            _userRole.AddRole(RoleName);
        }
    }
}