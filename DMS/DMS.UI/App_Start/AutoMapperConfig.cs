using AutoMapper;
//using DMS.Automappers.Profiles;
using DMS.DAL.DatabaseContext;
using DMS.DAL.EntityModels;
using DMS.UI.ViewModels;
using DMS.UI.ViewModels.ErrorLog;
using DMS.ViewModels;
using System.Reflection;

namespace DMS.UI.App_Start
{
    public class AutoMapperConfig
    {
        public static void Config()
        {
            Mapper.Initialize(config =>
            {
                #region Action Role

                config.CreateMap<ActionRole, ActionRoleVM>().ReverseMap();
                config.CreateMap<ActionRole, ActionRoleVM>()
                     .ForMember(x => x.controllerActionVm, y => y.MapFrom(z => z.ControllerAction));
                //.ForMember(x => x.applicationRoleVm, y => y.MapFrom(z => z.ApplicationRole));

                #endregion Action Role

                #region Application Role

                config.CreateMap<ApplicationRole, ApplicationRoleVM>().ReverseMap();
                config.CreateMap<ApplicationRole, ApplicationRoleVM>()
                     .ForMember(x => x.actionRoleVm, y => y.MapFrom(z => z.ActionRole));
                //.ForMember(x => x.menuListVm, y => y.MapFrom(z => z.MenuRole));

                #endregion Application Role

                #region Application User

                config.CreateMap<ApplicationUser, ApplicationUserVM>().ReverseMap();
                config.CreateMap<ApplicationUser, ApplicationUserVM>();

                #endregion Application User

                #region Admin Part

                /// map For Controller Action
                config.CreateMap<ControllerAction, ControllerActionVM>().ReverseMap();
                config.CreateMap<ControllerAction, ControllerActionVM>();


                // Map For Menu
                config.CreateMap<MenuList, MenuListVM>().ReverseMap();
                config.CreateMap<MenuList, MenuListVM>();
                #endregion

                #region Log Part

                config.CreateMap<GlobalErrorLog, GlobalErrorLogVM>().ReverseMap();
                config.CreateMap<GlobalErrorLog, GlobalErrorLogVM>();

                #endregion Log Part



                //config.CreateMap<Customer, Customer_VM>().ReverseMap();
                //config.CreateMap<Customer, Customer_VM>();
                //.ForMember(x => x.customer_CompanyVM, y => y.MapFrom(z => z.Customer_Company))
                //.ForMember(x => x.customer_PersonVM, y => y.MapFrom(z => z.Customer_Person))
                //.ForMember(x => x.District_VM, y => y.MapFrom(z => z.District));

                //scan allprofiles and add automatically
                // ... or static approach:
                config.AddProfiles(Assembly.GetExecutingAssembly());
            });

            //Mapper.AssertConfigurationIsValid();
        }
    }
}