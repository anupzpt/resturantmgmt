using DMS.DAL.EntityModels;
using DMS.DAL.DatabaseContext;
using System.Collections.Generic;

namespace DMS.DAL.Repositories.GeneralRepo.Interfaces
{
    public interface IApplicationUserRoleRepo
    {
        List<ApplicationRole> GetAllRoleList();
        void AddUserNameAndRoleForCompany(ApplicationUser user, string roleName);
        ApplicationRole GetRoleFilterByName(string RoleName);
        void SaveEditedRole(ApplicationRole Role);
        string GetRoleNameById(string Id);
        ApplicationRole GetRoleDetailsById(string Id);
        void SaveUserWithRole(ApplicationUser model);
        List<ApplicationUser> GetAllUserList();
        ApplicationUser GetUserByUserName(string UserName);
        ApplicationUser GetUserById(string Id);
        //void SaveEditedUserWithRole(ApplicationUser model);
        void RemoveUser(string Id);
        //void SaveRoleForMenu(List<MenuRole> roleWithMenu);
        //void RemoveRole(string Id);
        //void UpdateUserListWithRole(ApplicationRole _role);
    }
}
