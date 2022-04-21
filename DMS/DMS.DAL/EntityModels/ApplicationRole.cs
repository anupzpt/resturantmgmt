using DMS.DAL.DatabaseContext;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace DMS.DAL.EntityModels
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
        {
            //this.MenuRole = new HashSet<MenuRole>();
            this.ActionRole = new HashSet<ActionRole>();
        }

        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string CreatedNepaliDate { get; set; }
        public string CreatedEnglishDate { get; set; }
        public string UpdatedNepaliDate { get; set; }
        public string UpdatedEnglishDate { get; set; }
        public string UpdatedBy { get; set; }
        //public virtual ICollection<MenuRole> MenuRole { get; set; }
        public virtual ICollection<ActionRole> ActionRole { get; set; }
    }
}