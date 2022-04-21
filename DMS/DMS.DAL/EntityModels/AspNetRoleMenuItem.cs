using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.EntityModels
{
    public class AspNetRoleMenuItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual ApplicationRole Role { get; set; }
        public int MenuListId { get; set; }
        [ForeignKey("MenuListId")]
        public virtual MenuList MenuList { get; set; }

    }
}
