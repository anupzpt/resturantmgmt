using DMS.DAL.DatabaseContext;
using DMS.DAL.EntityModels;
using DMS.DAL.Repositories.GeneralRepo.Interfaces;
using DMS.DAL.StaticHelper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace DMS.DAL.Repositories.GeneralRepo.Implementation
{
    public class ApplicationUserRoleRepo : IApplicationUserRoleRepo
    {
        private readonly IdentityEntities _db = new IdentityEntities();
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public ApplicationUserRoleRepo()
        {
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));
            _roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(_db));
        }

        public void AddUserNameAndRoleForCompany(ApplicationUser user, string roleName)
        {
            var userDetails = _userManager.FindByEmail(user.Email);

            if (userDetails == null)
            {
                _userManager.Create(user, user.PasswordHash);
                string roleId = _roleManager.FindByName(roleName).Id;
                var result = _userManager.AddToRole(user.Id, roleName);
            }
        }

        public List<ApplicationRole> GetAllRoleList()
        {
            var alldata = _roleManager.Roles.ToList();
            return alldata;
        }

        public string GetRoleNameById(string Id)
        {
            var roleDetails = _roleManager.FindById(Id);
            return roleDetails.Name;
        }

        public ApplicationRole GetRoleFilterByName(string RoleName)
        {
            var roleDetails = _roleManager.FindByName(RoleName);
            return roleDetails;
        }

        public ApplicationRole GetRoleDetailsById(string Id)
        {
            var roleDetails = _roleManager.FindById(Id);
            return roleDetails;
        }

        public void SaveEditedRole(ApplicationRole Role)
        {
            var EditedListOfControllerActionRole = Role.ActionRole.Where(x => x.Id != null).ToList();
            //This is used For Added New Role On User
            var NewAddedListOfControllerActionRole = Role.ActionRole.Where(x => x.Id == null && x.IsActive == true).ToList();
            try
            {
                if (Role.Name != null)
                {
                    var roleDetails = _roleManager.FindById(Role.Id);
                    if (roleDetails.Name != Role.Name)
                    {
                        roleDetails.Name = Role.Name;
                        _roleManager.Update(roleDetails);
                    }
                    foreach (var item in EditedListOfControllerActionRole)
                    {
                        var actionRoleDetails = _db.ActionRoles.FirstOrDefault(x => x.Id == item.Id);
                        if (actionRoleDetails.IsActive != item.IsActive)
                        {
                            actionRoleDetails.IsActive = item.IsActive;
                            actionRoleDetails.UpdatedNepaliDate = SystemInfo.EnglishDate;
                            _db.SaveChanges();
                        }
                    }
                    foreach (var item in NewAddedListOfControllerActionRole)
                    {
                        item.IsActive = item.IsActive;
                        item.CreatedNepaliDate = SystemInfo.NepaliDate;
                        item.UpdatedNepaliDate = SystemInfo.EnglishDate;
                        _db.ActionRoles.Add(item);
                        _db.SaveChanges();
                    }
                }
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
            }
        }

        public void SaveUserWithRole(ApplicationUser _user)
        {
            ApplicationUser user = _userManager.FindByName(_user.UserName);
            if (user == null)
            {
                user.CreatedNepaliDate = SystemInfo.NepaliDate;
                user.UpdatedNepaliDate = SystemInfo.NepaliDate;
                IdentityResult userResult = _userManager.Create(user, _user.PasswordHash);
                try
                {
                    if (userResult.Succeeded)
                    {
                        foreach (var item in _user.Roles)
                        {
                            var result = _userManager.AddToRole(user.Id, item.RoleId);
                        }
                    }
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
                }
            }
        }

        public List<ApplicationUser> GetAllUserList()
        {
            var data = _userManager.Users.ToList();
            return data;
        }

        public ApplicationUser GetUserById(string Id)
        {
            var data = _userManager.FindById(Id);
            return data;
        }

        public void SaveEditedUserWithRole(ApplicationUser user)
        {
            ApplicationUser users = _userManager.FindById(user.Id);
            if (users != null)
            {
                var roles = _userManager.GetRoles(user.Id);
                _userManager.RemoveFromRoles(user.Id, roles.ToArray());
                foreach (var item in user.Roles)
                {
                    var isinRole = _userManager.IsInRole(user.Id, item.RoleId);
                    if (isinRole == false)
                    {
                        var result = _userManager.AddToRole(user.Id, item.RoleId);
                    }
                }
            }
        }

        public ApplicationUser GetUserByUserName(string UserName)
        {
            var userDetails = _userManager.FindByName(UserName);
            return userDetails;
        }

        public void RemoveUser(string Id)
        {
            var userDetails = _userManager.FindById(Id);
            userDetails.IsActive = false;
            userDetails.IsDelete = true;
            userDetails.UpdatedNepaliDate = SystemInfo.NepaliDate;
            _userManager.Update(userDetails);
        }

        public void RemoveRole(string Id)
        {
            var roleDetails = _roleManager.FindById(Id);
            roleDetails.IsActive = false;
            roleDetails.UpdatedNepaliDate = SystemInfo.NepaliDate;
            _roleManager.Update(roleDetails);
        }

        //public void SaveRoleForMenu(List<MenuRole> roleWithMenu)
        //{
        //    foreach (var item in roleWithMenu)
        //    {
        //        var menuRole = _db.MenuRoles.Where(x => x.MenuId == item.MenuId && x.RoleId == item.RoleId).FirstOrDefault();
        //        if (menuRole != null)
        //        {
        //            menuRole.UpdatedNepaliDate = SystemInfo.NepaliDate;
        //            menuRole.IsActive = item.IsActive;
        //            _db.SaveChanges();
        //        }
        //        else if (item.IsActive == true)
        //        {
        //            item.CreatedNepaliDate = SystemInfo.NepaliDate;
        //            item.UpdatedNepaliDate = SystemInfo.NepaliDate;
        //            _db.MenuRoles.Add(item);
        //            _db.SaveChanges();
        //        }
        //    }
        //}

        public void UpdateUserListWithRole(ApplicationRole _role)
        {
            var roleDetails = _roleManager.FindById(_role.Id);
            if (roleDetails != null)
            {
                var oldUserList = roleDetails.Users.ToList();

                foreach (var user in oldUserList)
                {
                    var isinRole = _userManager.IsInRole(user.UserId, roleDetails.Name);
                    if (isinRole == true)
                    {
                        var result = _userManager.RemoveFromRole(user.UserId, roleDetails.Name);
                    }
                }
                foreach (var user in _role.Users)
                {
                    var result = _userManager.AddToRole(user.UserId, roleDetails.Name);
                }
            }
        }
    }
}