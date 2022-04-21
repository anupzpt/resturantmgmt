using DMS.DAL.EntityModels;
using DMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Services.General.Interface
{
    public interface IRolePermissionServices
    {
        void CreateMenuListbyEmpType(string Emp_Type, int[] Perm1);
        List<AspNetRoleMenuItem> GetApplicationUserMenuList();
    }
}
