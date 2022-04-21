using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DMS.UI.ViewModels
{
    public class ControllerActionVM
    {
        public int Id { get; set; }
        [Display(Name = "Controller Name")]
        public string ControllerName { get; set; }
        public string Title { get; set; }
        [Display(Name = "Action Name")]
        public string ActionName { get; set; }
        [Display(Name = "Work ALL Time")]
        public bool ActiveAllTime { get; set; }
        public string Attributes { get; set; }
        [Display(Name = "Return Type")]
        public string ReturnType { get; set; }
        [Display(Name = "Status")]
        public bool IsActive { get; set; }
        public ICollection<ActionRoleVM> ControllerActionRoleList { get; set; }
        public List<ControllerActionVM> controllerActionList { get; set; }
    }
}