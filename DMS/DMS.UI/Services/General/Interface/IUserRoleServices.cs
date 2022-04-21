using DMS.DAL.EntityModels;
using DMS.DAL.Repositories.GenericRepo;
using DMS.UI.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMS.UI.Services.General.Interface
{
    public interface IUserRoleServices : IGenericRepo<ApplicationRole, string>
    {
        //Task<ApplicationRoleVM> GetAllRoleListFilterWithRoleName(string RoleName);

        void SaveEditedRoleWithControllerList(ApplicationRoleVM Role);

        //ApplicationRoleVM GetDetailsOfRoleWithControllerAction(string id);

        //void SaveUserWithRole(ApplicationUserVM _UserVM);

        //Task<ApplicationUserVM> GetUserWithRoleList(string id);

        //void SaveEditedUserWithRoles(ApplicationUserVM _userVm);

        //ApplicationUserVM GetUserByUserName(string UserName);

        //RoleMenuVM RoleForMenu(int menuId);
        //void SaveRoleForMenu(MenuListVM roleWithMenu);

        //void SaveRoleWithMultipleMenu(ApplicationRoleVM role);

        ApplicationRoleVM GetUserListWithRole(string roleId);

        //void UpdateUserListWithRole(ApplicationRoleVM role);

        //void AddNewRoleWithMultipleMenu(ApplicationRoleVM applicationRoleVm);

        //ApplicationUserVM GetUserById(string id);

        List<ApplicationUserVM> GetAllUserList();

        void AddRole(string RoleName);
        bool RoleExists(string RoleName);
    }
}