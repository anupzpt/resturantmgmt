using DMS.DAL.DatabaseContext;
using DMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DMS.Services.General.Interface
{
    public interface IEmployeeGroupService
    {
        Task<List<VMEmployeeGroup>> GetAllEmployeeVM();
        Task<List<VMEmployeeGroup>> GetAllValidEmployeeVM();
        void AddEmployee(VMEmployeeGroup Employee_VM);
        void EditEmployee(VMEmployeeGroup Employee_VM);
        Task<VMEmployeeGroup> GetDetails(int id);
        VMEmployeeGroup GetDetailVM(int id);
        Task<IEnumerable<SelectListItem>> GetDropwDown(int Id);
        Task<IList<VMEmployeeGroup>> GetListByEmpID(int id);

    }
}
