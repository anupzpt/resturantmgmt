using DMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Services.General.Interface
{
    public interface IDepartmentServices
    {
        Task<List<Department_VM>> GetAllDepartmentVM();
        Task<List<Department_VM>> GetAllValidDepartmentVM();
        void AddDepartment(Department_VM Department_VM);
        void DeleteDepartment(int id);
        void EditDepartment(Department_VM Department_VM);
        Task<Department_VM> GetDepartmentbyId(int id);
    }
}
