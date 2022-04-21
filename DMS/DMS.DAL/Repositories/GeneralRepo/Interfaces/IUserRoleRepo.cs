using DMS.DAL.DatabaseContext;
using DMS.DAL.EntityModels;
using System.Collections.Generic;

namespace DMS.DAL.Repositories.GeneralRepo.Interfaces
{
    public interface IUserRoleRepo
    {
        void SaveEditedUserWithRoles(ApplicationUser userVm);
        void UpdateUserListWithRole(List<ApplicationUser> aspUser, string roleId);
        //void SaveRoleForMultipleMenu(List<MenuRole> roleWithMultiplerMenu);
        void SaveEditedRoleWithActionList(List<ActionRole> actionRoleList);
        bool AddUserToRoles(ApplicationUser RegUser, EnumUserRoles RoleName);
        void AddRole(string RoleName);
        bool RoleExists(string RoleName);
    }
}
