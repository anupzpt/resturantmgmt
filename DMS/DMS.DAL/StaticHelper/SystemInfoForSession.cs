using DMS.DAL.DatabaseContext;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.StaticHelper
{
    public class SystemInfoForSession
    {
        //public bool IsAdmin;

        public string UserName { get; set; }
        public string UserId { get; set; }
        public Array RoleIds { get; set; }
        public string RoleId { get; set; }
        public string TokenNo { get; set; }
        public bool IsAdmin { get; set; }

        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }

        public int DocHandlerID { get; set; }

        //public employee_VM EmployeeVM { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        //public string[] EmployeeTypes { get; set; }
        public int[] AssociatedGroups { get; set; }
        public string[] AssociatedGroupName { get; set; }

        //public bool IsHOAdmin { get; set; }
        public bool IsOrgAdmin => RoleInfoEnum != null && (RoleInfoEnum.Contains(EnumUserRoles.OrganizationAdmin.ToString()));
        public bool IsSuperAdmin => RoleInfoEnum != null && (RoleInfoEnum.Contains(EnumUserRoles.SuperAdmin.ToString()));
        public bool IsOrgUser => IsOrgAdmin || (RoleInfoEnum != null && (RoleInfoEnum.Contains(EnumUserRoles.OrganizationUser.ToString())));
        public bool IsBranchUser => RoleInfoEnum != null && (RoleInfoEnum.Contains(EnumUserRoles.BranchAdmin.ToString()) || RoleInfoEnum.Contains(EnumUserRoles.BranchUser.ToString()));
        public bool IsBranchAdmin => RoleInfoEnum != null && (RoleInfoEnum.Contains(EnumUserRoles.BranchAdmin.ToString()));
        public bool CanCheckAllOrgData => IsSuperAdmin || IsOrgAdmin;

        public string[] RoleInfoId { get; set; }
        public string[] RoleInfoEnum { get; set; }
        public DateTime SysDateEng => DateTime.Now;
        public string SysDateNep => NepaliCalender.Convert.ToNepali(SysDateEng);
        [JsonIgnore]
        public bra01branches bra01branches { get; set; }
        public bool EnableContactLessCard { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}