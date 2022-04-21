using System.ComponentModel.DataAnnotations;

namespace DMS.UI.ViewModels
{
    public class ActionRoleVM
    {
        public string Id { get; set; }
        public int ActionId { get; set; }
        public bool IsActive { get; set; }
        public string RoleId { get; set; }
        [Display(Name = "Controller Name")]
        public string ControllerName { get; set; }
        [Display(Name = "Action Name")]
        public string ActionName { get; set; }
        public string CreatedNepaliDate { get; set; }
        public string CreatedEnglishDate { get; set; }
        public string UpdatedNepaliDate { get; set; }
        public string UpdatedEnglishDate { get; set; }
        public string UpdatedBy { get; set; }
        public ControllerActionVM controllerActionVm { get; set; }
        public ApplicationRoleVM applicationRoleVm { get; set; }
    }

}