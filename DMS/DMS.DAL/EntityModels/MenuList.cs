using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMS.DAL.EntityModels
{
    public class MenuList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? ControllerActionId { get; set; }
        [ForeignKey("ControllerActionId")]
        public virtual ControllerAction ControllerAction { get; set; }
        public int? ParentId { get; set; }
        public string DropDownName { get; set; }
        public int Position { get; set; }
        public string IconName { get; set; }
        public bool IsActive { get; set; }
        public string CreatedNepaliDate { get; set; }
        public string CreatedEnglishDate { get; set; }
        public string UpdatedNepaliDate { get; set; }
        public string UpdatedEnglishDate { get; set; }
        public string UpdatedBy { get; set; }
        public byte? MenuType { get; set; }
        public byte? Area_id { get; set; }

        //public virtual ICollection<MenuRole> MenuRole { get; set; }

        //public virtual ICollection<MenuRole> MenuRole { get; set; }
    }
}