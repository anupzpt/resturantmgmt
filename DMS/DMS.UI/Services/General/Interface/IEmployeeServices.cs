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
    public interface IEmployeeServices
    {
        Task<List<Employee_VM>> GetAllEmployeeVM();
        Task<List<Employee_VM>> GetAllValidEmployeeVM();
        Task<bool> AddEmployee(Employee_VM Employee_VM);
        Task<bool> DeleteEmployee(int id);
        Task<bool> EditEmployee(Employee_VM Employee_VM);
        Task<Employee_VM> GetEmployeebyId(int id);
        Employee_VM GetEmployeebyIdSync(int id);
        Task<IEnumerable<Employee_VM>> GetEmployeebyEmpType(string EmpType);
        Task<IEnumerable<SelectListItem>> GetEmployeeDropwDown(int Id, int CurrentEmpId, int InitiatorEmpId);
        List<VMEmployeeRoles> GetEmployeeTypeList();

        Task<IEnumerable<Employee_VM>> GetLoanInitiatorList();
        //Task<IEnumerable<Employee_VM>> GetLoanRecommenderList();
        //Task<IEnumerable<Employee_VM>> GetLoanApproverList();
        Task<IEnumerable<Employee_VM>> GetCADSupervisorList();
        Task<IEnumerable<Employee_VM>> GetCADApproverList();
        Task<IEnumerable<Employee_VM>> GetCADLegalInitiatorList();
        Task<IEnumerable<Employee_VM>> GetCADLegalSupervisorList();
        Task<IEnumerable<Employee_VM>> GetCADLegalHeadApproverList();
        Task<IEnumerable<Employee_VM>> GetCADLegalApproverList();
        Task<IEnumerable<Employee_VM>> GetCADFinalApproverList();
    }
}
