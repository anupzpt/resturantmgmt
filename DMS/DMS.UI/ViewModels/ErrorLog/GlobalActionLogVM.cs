using System;
using System.ComponentModel.DataAnnotations;

namespace DMS.UI.ViewModels.ErrorLog
{
    public class GlobalActionLogVM
    {
        public string Id { get; set; }
        public string Log { get; set; }

        [Display(Name = "Controller Name")]
        public string ControllerName { get; set; }

        [Display(Name = "Action Name")]
        public string ActionPerformed { get; set; }

        public string Reason { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdateUserName { get; set; }
        public DateTime Date { get; set; }
    }
}