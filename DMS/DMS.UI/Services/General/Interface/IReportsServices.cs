using DMS.DAL.DatabaseContext;
using DMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DMS.Services.General.Interface
{
    public interface IReportsServices
    {
        List<Loan_Request_VM> GetLoanRequestDetails(ReportsVM reportsVM);
        List<Employee_VM> GetEmployeeList(ReportsVM reportsVM);

        List<Loan_Request_VM> Top10LoanRequestDetails(string Status);
        List<Task_Info_VM> Top10Task();
        List<Memo_VM> Top10Memo();
        List<Loan_Request_File_Path_VM> GetFileListByCustomer(ReportsVM ReportsVM);
        List<Loan_Request_File_Path_VM> GetLast10UploadedFiles();
        List<Cad_Secondary_InformationVM> GetCADInformationReportList(Cad_Secondary_InformationVM cadinfo,bool atleastone);
    }
}