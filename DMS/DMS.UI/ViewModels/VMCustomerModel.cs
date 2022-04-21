using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DMS.ViewModels
{
    public class Customer_VM
    {
        public int Id { get; set; }
        public string CBSCode { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string MobileNumber { get; set; }
        public string IsForeigner { get; set; }
        public string Address { get; set; }
        public string Branch { get; set; }
        public string District { get; set; }
        public string CustomerType { get; set; }
        public string Status { get; set; }
        public DateTime DateEng { get; set; }

        [Display(Name="Created Date")]
        public string FormatCreatedDate { get; set; }

    }
}
