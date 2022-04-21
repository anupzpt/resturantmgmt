using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMS.DAL.EntityModels
{
    public class ActionRole
    {
        [Key]
        public string Id { get; set; }
        public int ActionId { get; set; }
        [ForeignKey("ActionId")]
        public virtual ControllerAction ControllerAction { get; set; }
        public string RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual ApplicationRole ApplicationRole { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string CreatedNepaliDate { get; set; }
        public string CreatedEnglishDate { get; set; }
        public string UpdatedNepaliDate { get; set; }
        public string UpdatedEnglishDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}