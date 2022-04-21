using DMS.DAL.DatabaseContext;
using DMS.DAL.EntityModels;
using DMS.DAL.Repositories.GeneralRepo.Interfaces;
using DMS.DAL.StaticHelper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace DMS.DAL.Repositories.GeneralRepo.Implementation
{
    public class UserRoleRepo : IUserRoleRepo
    {
        private IdentityEntities _db = new IdentityEntities();
        private UserManager<ApplicationUser> userManager;
        private RoleManager<ApplicationRole> roleManager;

        public UserRoleRepo()
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));
            roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(_db));
        }

        //
        public void SaveEditedRoleWithActionList(List<ActionRole> actionRoleList)
        {
            try
            {
                foreach (var item in actionRoleList)
                {
                    var itemDetails = _db.ActionRoles.FirstOrDefault(x => x.ActionId == item.ActionId && x.RoleId == item.RoleId);
                    if (itemDetails == null && item.IsActive)
                    {
                        itemDetails = new ActionRole();
                        itemDetails.IsActive = true;
                        itemDetails.ActionId = item.ActionId;
                        itemDetails.RoleId = item.RoleId;
                        itemDetails.Id = Guid.NewGuid().ToString();
                        itemDetails = SystemInfo.CreateDates(itemDetails);
                        _db.ActionRoles.Add(itemDetails);
                        _db.SaveChanges();
                    }
                    else if (itemDetails != null)
                    {
                        itemDetails.IsActive = item.IsActive;
                        itemDetails = SystemInfo.UpdateDates(itemDetails);
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

        public void SaveEditedUserWithRoles(ApplicationUser user)
        {
            ApplicationUser users = userManager.FindById(user.Id);
            if (users != null)
            {
                var roles = userManager.GetRoles(user.Id);
                userManager.RemoveFromRoles(user.Id, roles.ToArray());
                foreach (var item in user.Roles)
                {
                    var roleDetails = roleManager.FindById(item.RoleId);
                    var isinRole = userManager.IsInRole(user.Id, roleDetails.Name);
                    if (isinRole == false)
                    {
                        var result = userManager.AddToRole(user.Id, roleDetails.Name);
                    }
                }
            }
        }

        //public void SaveRoleForMultipleMenu(List<MenuRole> roleWithMultiplerMenu)
        //{
        //    foreach (var item in roleWithMultiplerMenu)
        //    {
        //        var menuRole = _db.MenuRoles.Where(x => x.MenuId == item.MenuId && x.RoleId == item.RoleId).FirstOrDefault();
        //        if (menuRole != null)
        //        {
        //            menuRole.IsActive = item.IsActive;
        //            menuRole = SystemInfo.UpdateDates(menuRole);
        //        }
        //        else if (item.IsActive == true)
        //        {
        //            _db.MenuRoles.Add(SystemInfo.CreateDates(item));
        //        }
        //        _db.SaveChanges();
        //    }
        //}

        public void UpdateUserListWithRole(List<ApplicationUser> aspUser, string roleId)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));
            var roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(_db));
            var roleDetails = roleManager.FindById(roleId);
            if (roleDetails != null)
            {
                var oldUserList = roleDetails.Users.ToList();
                foreach (var user in oldUserList)
                {
                    var isinRole = userManager.IsInRole(user.UserId, roleDetails.Name);
                    if (isinRole == true)
                    {
                        var result = userManager.RemoveFromRole(user.UserId, roleDetails.Name);
                    }
                }
                foreach (var user in aspUser)
                {
                    var result = userManager.AddToRole(user.Id, roleDetails.Name);
                }
            }
        }

        public bool AddUserToRoles(ApplicationUser RegUser, EnumUserRoles RoleName)
        {
         
            var result = userManager.AddToRole(RegUser.Id, RoleName.ToString());
            return result.Succeeded;
        }
        public IList<IdentityRole> RoleList()
        {
            var roleStore = new RoleStore<IdentityRole>(_db);
            var roleMngr = new RoleManager<IdentityRole>(roleStore);

            var roles = roleMngr.Roles.ToList();
            return roles;
        }
        public bool RoleExists(string RoleName)
        {
            return roleManager.RoleExists(RoleName);
            // creating Creating Manager role    

        }
        public void AddRole(string RoleName)
        {
            if (!RoleExists(RoleName))
            {
                var role = new ApplicationRole()
                {
                    Name = RoleName,
                    IsActive = true,
                    IsDelete = false,
                    CreatedEnglishDate = SystemInfo.EnglishDate,
                    CreatedNepaliDate = SystemInfo.NepaliDate,
                    UpdatedBy = SessionHelper.GetSession().UserName,
                    UpdatedEnglishDate = SystemInfo.EnglishDate,
                    UpdatedNepaliDate = SystemInfo.NepaliDate,
                };
                role.Name = RoleName;
                roleManager.Create(role);
            }
        }
    }
}