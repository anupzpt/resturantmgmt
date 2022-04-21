using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMS.ViewModels.VMEmail
{
    public class VMEditSettings
    {
        public string SMTPServer { get; set; }
        public string SMTPUser { get; set; }
        public string SMTPPassword { get; set; }
        public int SMTPPort { get; set; }
        public bool SMTPSSL { get; set; }
        public string SMTPFrom { get; set; }
        public bool EmailNotification { get; set; }
    }
}