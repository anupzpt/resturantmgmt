using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMS.ViewModels.VMEmail
{
    public class VMBaseEmailFormat
    {
        public string EmployeeName { get; set; }
        public string LogoURL { get; set; }
        public string Link { get; set; }
        public string FromEmployeeName { get; set; }
        public VMBaseEmailFormat()
        {
        }
    }
    public class VMInitiatedEmailFormat : VMBaseEmailFormat
    {

    }
    public class VMRecommendedEmailFormat : VMBaseEmailFormat
    {

    }
    public class VMReturnedEmailFormat : VMBaseEmailFormat
    {

    }
    public class VMRejectedEmailFormat : VMBaseEmailFormat
    {

    }
    public class VMApprovedEmailFormat : VMBaseEmailFormat
    {

    }
}