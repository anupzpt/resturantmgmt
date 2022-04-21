using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DMS.UI.ViewModels
{
    public class ApplicationUserVM
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public string Id { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string PasswordHash { get; set; }
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public string CreatedNepaliDate { get; set; }
        public string CreatedEnglishDate { get; set; }
        public string UpdatedNepaliDate { get; set; }
        public string UpdatedEnglishDate { get; set; }
        public string UpdatedBy { get; set; }
        public List<ApplicationRoleVM> RoleList { get; set; }
        public bool IsAssignRole { get; set; }

        public class UserName_Role
        {
            public UserName_Role()
            {
                UserDetails = new List<ApplicationUserVM>();
                AspNetRoleList = new List<ApplicationRoleVM>();
            }
            public string UserName { get; set; }
            public bool IsTrueRole { get; set; }
            [Display(Name = "Role Name")]
            public string RoleName { get; set; }
            public List<ApplicationUserVM> UserDetails { get; set; }
            public List<ApplicationRoleVM> AspNetRoleList { get; set; }
            public List<SelectListItem> GetAllUserDtailsList { get; set; }

        }
    }
}