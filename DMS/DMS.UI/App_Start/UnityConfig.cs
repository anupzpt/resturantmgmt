using DMS.DAL.DatabaseContext;
using DMS.DAL.EntityModels;
using DMS.DAL.Helpers;
using DMS.DAL.Interface;
using DMS.DAL.Interfaces;
using DMS.DAL.Repositories.GeneralRepo.Implementation;
using DMS.DAL.Repositories.GeneralRepo.Interfaces;
using DMS.DAL.Repositories.GenericRepo;
using DMS.DAL.Repositories.MainRepo;
using DMS.Services;
using DMS.Services.General.Implementation;
using DMS.Services.General.Interface;
using DMS.UI.Controllers;
using DMS.UI.Services.General.Implementation;
using DMS.UI.Services.General.Interface;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;
using Unity.Lifetime;

namespace DMS
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();

            container.RegisterType<MainEntities, MainEntities>(new PerRequestLifetimeManager());

            container.RegisterType<ApplicationSignInManager, ApplicationSignInManager>();
            container.RegisterType<ApplicationUserManager, ApplicationUserManager>();
            container.RegisterType<DbContext, IdentityEntities>(new HierarchicalLifetimeManager());
            //container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<UserManager<ApplicationUser>>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new HierarchicalLifetimeManager());
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));



            //Services
            container.RegisterType<CFGenericRepo<ActionRole, string>, CFGenericRepo<ActionRole, string>>();

            container.RegisterType<CFGenericRepo<ApplicationRole, string>, CFGenericRepo<ApplicationRole, string>>();
            container.RegisterType<ICFGenericRepo<GlobalErrorLog, string>, CFGenericRepo<GlobalErrorLog, string>>();
            //Services
            container.RegisterType<IRolePermissionServices, RolePermissionServices>();
            container.RegisterType<IMenuServices, MenuServices>();
            container.RegisterType<IControllerActionServices, ControllerActionServices>();
            container.RegisterType<IUserRoleServices, UserRoleServices>();
            container.RegisterType<IControllerActionRepo, ControllerActionRepo>();
            container.RegisterType<AuthHelper, AuthHelper>();
            container.RegisterType<IBranchesRepo, BranchesRepo>();
            container.RegisterType<IUserRepo, UserServiceRepo>();
            container.RegisterType<IConfigValuesRepo, ConfigValuesRepo>();
            container.RegisterType<IConfigValuesByEnumRepo, ConfigValuesByEnumRepo>();
            container.RegisterType<IApplicationUserRoleRepo, ApplicationUserRoleRepo>();
            container.RegisterType<IUserCodesRepo, UserCodesRepo>();
            container.RegisterType<IBranchesRepo, BranchesRepo>();
            container.RegisterType<IDepartmentRepo, DepartmentRepo>();
            container.RegisterType<IDesignationRepo, DesignationRepo>();
            container.RegisterType<IEmployeeRepo, EmployeeRepo>();
            container.RegisterType<ILevelsRepo, LevelsRepo>();
            container.RegisterType<IUserRepo, UserServiceRepo>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

        }
    }
}