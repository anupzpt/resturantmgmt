using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DMS.UI.ViewModels
{
    public class ApplicationRoleVM
    {
        public string Id { get; set; }
        [Display(Name = "Role Name")]
        public string Name { get; set; }
        public bool IsChecked { get; set; }
        public string CreatedNepaliDate { get; set; }
        public string CreatedEnglishDate { get; set; }
        public string UpdatedNepaliDate { get; set; }
        public string UpdatedEnglishDate { get; set; }
        public string UpdatedBy { get; set; }
        public List<ControllerActionVM> ControllerActionList { get; set; }
        public List<ActionRoleVM> ControllerActionRoleList { get; set; }
        public List<MenuListVM> menuListVm { get; set; }
        public List<ApplicationUserVM> applicationUserVm { get; set; }
        public List<ActionRoleVM> actionRoleVm { get; set; }

    }
    #region RoleIdVM
    public class RoleIdVM
    {
        public string Id { get; set; }
    }
    #endregion
}