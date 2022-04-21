using DMS.DAL.DatabaseContext;
using DMS.DAL.EntityModels;
using DMS.DAL.StaticHelper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMS.UI.App_Start
{
    public class UserManagement
    {
        internal static void IdentitySeed()
        {
            string cPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Private", "Migrations");
            if (!System.IO.Directory.Exists(cPath)) { System.IO.Directory.CreateDirectory(cPath); }
            try
            {
                string fPath = System.IO.Path.Combine(cPath, $"Migration_{DateTime.Now.ToString("yyyyMMdd")}.txt");


                IdentityEntities context = new IdentityEntities();
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                string UserId = "";

                #region UserRole

                if (!roleManager.RoleExists("SuperAdmin"))
                {
                    var role = new ApplicationRole()
                    {
                        Name = "SuperAdmin",
                        IsActive = true,
                        IsDelete = false,
                        CreatedEnglishDate = SystemInfo.EnglishDate,
                        CreatedNepaliDate = SystemInfo.NepaliDate,
                        UpdatedBy = UserId,
                        UpdatedEnglishDate = SystemInfo.EnglishDate,
                        UpdatedNepaliDate = SystemInfo.NepaliDate,
                    };
                    var roleresult = roleManager.Create(role);
                }
                if (!roleManager.RoleExists("Admin"))
                {
                    var role = new ApplicationRole()
                    {
                        Name = "Admin",
                        IsActive = true,
                        IsDelete = false,
                        CreatedEnglishDate = SystemInfo.EnglishDate,
                        CreatedNepaliDate = SystemInfo.NepaliDate,
                        UpdatedBy = UserId,
                        UpdatedEnglishDate = SystemInfo.EnglishDate,
                        UpdatedNepaliDate = SystemInfo.NepaliDate,
                    };
                    var roleresult = roleManager.Create(role);
                }

                ApplicationUser user = userManager.FindByName("SuperAdmin");
                if (user == null)
                {
                    user = new ApplicationUser();
                    user.UserName = "SuperAdmin";
                    user.Email = Properties.Settings.Default.SuperAdminUser;
                    IdentityResult userResult = userManager.Create(user, Properties.Settings.Default.SuperAdminPassword);
                    if (userResult.Succeeded)
                    {
                        var result = userManager.AddToRole(user.Id, "SuperAdmin");
                    }
                }

                #endregion UserRole
            }
            catch (Exception ex)
            {
                string fPath = System.IO.Path.Combine(cPath, $"Migration_error_{DateTime.Now.ToString("yyyyMMdd")}.txt");
                string msg = (ex.InnerException != null ? ex.InnerException.Message : ex.Message) + ex.StackTrace;
                System.IO.File.AppendAllText(fPath, msg);
            }
        }
    }
}