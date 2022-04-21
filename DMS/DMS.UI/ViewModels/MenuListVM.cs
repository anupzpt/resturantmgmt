using DMS.DAL;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DMS.UI.ViewModels
{
    public class MenuListVM
    {
        public MenuListVM()
        {
            ControllerAction = new ControllerActionVM();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Display(Name = "Action Name")]
        public int ActionId { get; set; }
        [Display(Name = "Parent Menu")]
        public int ParentId { get; set; }
        [Display(Name = "Drop Down Name")]
        public string DropDownName { get; set; }
        public int Position { get; set; }
        [Display(Name = "Icon")]
        public string IconName { get; set; }
        [Display(Name = "Status")]
        [UIHint("ActiveInactive")]
        public bool IsActive { get; set; }

        [Display(Name = "Menu Type")]
        public byte? MenuType { get; set; }
        [Display(Name = "Area")]
        public byte? Area_id { get; set; }

        public string DisplayArea { get; set; }

        public string DisplayMenuType
        {
            get
            {
                string ret = "";
                //Male
                switch (MenuType)
                {
                    case (int)EnumMenuTypes.Primary:
                        ret = "Primary";
                        break;
                    case (int)EnumMenuTypes.Secondary:
                        ret = "Secondary";
                        break;
                }
                return ret;
            }
        }

        public string CreatedNepaliDate { get; set; }
        public string CreatedEnglishDate { get; set; }
        public string UpdatedNepaliDate { get; set; }
        public string UpdatedEnglishDate { get; set; }
        public string UpdatedBy { get; set; }
        public ControllerActionVM ControllerAction { get; set; }
        public ICollection<MenuListVM> ChildMenuList { get; set; }
        public List<ApplicationRoleVM> RolesList { get; set; }

        public bool RoleAccess { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string _ControllerActionId { get; set; }
    }

    public class MenuInfoVM
    {
        public MenuListVM menuListVM { get; set; }
        //User For Display The ControllerName
        public List<SelectListItem> controllerList { get; set; }
        //User For Display The ActionName
        public SelectList ActionList { get; set; }
        //User For Display The Parent Menu
        public SelectList MenuList { get; set; }
        public int ActionId { get; set; }
        public int Id { get; set; }
        public string DropDownName { get; set; }
        public string ParentId { get; set; }
        public bool IsActive { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        [Display(Name = "Icon Name")]
        public string IconName { get; set; }
        public byte? MenuType { get; set; }
        public byte? Area_id { get; set; }
    }
    public class MenuListInfo
    {
        public ICollection<MenuListVM> menuCollection { get; set; }
    }
}