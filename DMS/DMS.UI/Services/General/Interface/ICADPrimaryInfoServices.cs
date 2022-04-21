using DMS.DAL.DatabaseContext;
using DMS.DAL.Repositories.GenericRepo;
using DMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Services.General.Interface
{
    public interface ICADPrimaryInfoServices : IGenericRepo<CAD_Primary_info, int>
    {

        Task<CAD_Primary_infoVM> GetInfoByLoanRequestId(int LoanRequestId);
        void AddPrimaryInformation(Loan_Request_VM loanapproveinfo);
        void UpdateCADInformation(Cad_Secondary_InformationVM cadinfo);
        List<Cad_Secondary_InformationVM> CADInitiateReadyList(int LoanRequestId,int EmployeeId,int LoggedUserId);
        List<Cad_Secondary_InformationVM> CADInitiatedList(int LoanRequestId, int EmployeeId, int LoggedUserId);
        List<Cad_Secondary_InformationVM> CADSupervisedList(int LoanRequestId, int EmployeeId, int LoggedUserId);
        List<Cad_Secondary_InformationVM> CADLegalHeadAssignedList(int LoanRequestId, int EmployeeId, int LoggedUserId);
        List<Cad_Secondary_InformationVM> CADAdvisorApprovedList(int LoanRequestId, int EmployeeId, int LoggedUserId);
        List<Cad_Secondary_InformationVM> CADLegalHeadApprovedList(int LoanRequestId, int EmployeeId, int LoggedUserId);
        List<Cad_Secondary_InformationVM> CADCompletedList(int LoanRequestId, int EmployeeId, int LoggedUserId);

    }
}
