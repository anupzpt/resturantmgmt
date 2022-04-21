using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.DatabaseContext.MetaData
{
    class EmployeeMD
    {
        [Display(Name ="Employee ID")]
        public int emp01uin { get; set; }

        [Required(ErrorMessage = "Please enter Code")]
        [Display(Name ="Code")]
        public string emp01code { get; set; }

        [Display(Name = "Designation")]
        public int emp01des01uin { get; set; }

        [Display(Name = "Department")]
        public int emp01dep01uin { get; set; }

        [Display(Name = "Level")]
        public int emp01lvl01uin { get; set; }

        [Display(Name = "Branch")]
        public int emp01bra01uin { get; set; }

        [Required(ErrorMessage = "Please enter Name")]
        [Display(Name = "Name")]
        public string emp01name { get; set; }

        [Required(ErrorMessage = "Please enter JoinedDate")]
        [Display(Name = "JoinedDate")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime emp01join_date_eng { get; set; }

        [Display(Name = "Joined Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string emp01join_date_nep { get; set; }


        [Required(ErrorMessage = "Please enter Address")]
        [Display(Name = "Address")]
        public string emp01address { get; set; }

        [Required(ErrorMessage = "Please enter Email")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string emp01email { get; set; }

        [Required(ErrorMessage = "Please enter Mobile No")]
        [Display(Name = "Mobile")]
        [DataType(DataType.PhoneNumber)]
        public string emp01mobile { get; set; }

        [Display(Name = "Status")]
        public bool emp01status { get; set; }

        [Display(Name = "Deleted")]
        public bool emp01deleted { get; set; }

        [Display(Name = "CreatedBy")]
        public string emp01created_by { get; set; }

        [Display(Name = "CreatedDateEng")]
        public System.DateTime emp01created_date_eng { get; set; }

        [Display(Name = "CreatedDateNep")]
        public string emp01created_date_nep { get; set; }

        [Display(Name = "UpdatedBy")]
        public string emp01updated_by { get; set; }

        [Display(Name = "UpdatedDateNep")]
        public string emp01updated_date_nep { get; set; }

        [Display(Name = "UpdatedDateEng")]
        public System.DateTime emp01update_date_eng { get; set; }

        [Display(Name = "Branch")]
        public virtual bra01branches bra01branches { get; set; }

        [Display(Name = "Department")]
        public virtual dep01department dep01department { get; set; }

        [Display(Name = "Designation")]
        public virtual des01designations des01designations { get; set; }

        [Display(Name = "Level")]
        public virtual lvl01employee_levels lvl01employee_levels { get; set; }
    }
}
