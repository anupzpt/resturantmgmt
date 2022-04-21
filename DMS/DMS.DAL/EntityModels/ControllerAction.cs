using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMS.DAL.EntityModels
{
    public class ControllerAction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public bool ActiveAllTime { get; set; }
        public string Attributes { get; set; }
        public string ReturnType { get; set; }
        public string CreatedNepaliDate { get; set; }
        public string CreatedEnglishDate { get; set; }
        public string UpdatedNepaliDate { get; set; }
        public string UpdatedEnglishDate { get; set; }
        public string UpdatedBy { get; set; }
        public virtual ICollection<ActionRole> ActionRole { get; set; }
        public virtual ICollection<MenuList> MenuList { get; set; }
    }
}