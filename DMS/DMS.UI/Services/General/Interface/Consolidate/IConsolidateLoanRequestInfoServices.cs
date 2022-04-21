using DMS.DAL.DatabaseContext;
using DMS.DAL.Helpers;
using DMS.DAL.Repositories.GenericRepo;
using DMS.ViewModels;
using DMS.ViewModels.LoanRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Services.General.Interface
{
    public interface IConsolidateLoanRequestInfoServices : IGenericRepo<Loan_Request_Information, int>
    {
        Task<List<Loan_Request_VM>> GetAllLoanRequestDetail();
        Task<bool> UploadFile(Loan_Request_VM loan_Request_VM, String FileName, string FullPath, Customer_Document_Type_VM Cus_Doc_Type_VM, string CheckSumData);

        File_UploadInfo_VM getIconPath(File_UploadInfo_VM file_UploadInfo_VM);
        File_UploadInfo_VM DeleteFile(int file_Id);
        Task<List<Loan_Request_VM>> GetAllNonApproavedRejectedLoanRequestDetail();
        int AddLoanRequestInfo(Loan_Request_VMMini loan_Request_VM);
        Task<List<Loan_Request_VM>> GetAllLoanRequestDetailByRecommender();
        Task<Loan_Request_VM> GetAllLoanRequestDetailById(int id);
        Loan_Request_VM GetLoanRequestDetailVMById(int id);
        Loan_Request_Information GetLoanRequestDetailSyncById(int id);
        Task<List<Loan_Request_Information>> GetAllApproavedLoanRequestDetail();
        Task<bool> Edit(Loan_Request_VM loan_Request_VM);
        void LoanProcessing(Loan_Request_VM loan_Request_VM);
        Loan_Request_VM GetApprovalUnApprovalRemarks(int id);
        List<Employee_Status_VM> GetEmployeeStatus(Loan_Request_VM loan_Request_VM);
        Task<bool> SaveSignature(int id, string CriticalData, string Signature);
        Task<IList<DigitalSignature.SignatureInfo>> GetSignatures(string CriticalData);
        List<Loan_Request_VM> GetLoanRequestDetailByCustomerId(int customerId);
        List<Loan_Request_VM> GetAlloanRequestInformationVMListbyStatus(string status);
        List<Loan_Request_VM> GetAllPendingLoanRequestInfoVM();
        List<Loan_Request_VM> GetAllApprovedLoanRequestInfoVM();
        List<Loan_Request_VM> GetAllRejectedLoanRequestInfoVM();
        List<Loan_Request_VM> GetAllLoanRequestInformationbyStatus(string status);
        Loan_Request_File_Path GetFileInfo(int FileID);
        bool AddFileSignatureStamp(loan_request_file_signatures Data);
        IList<loan_request_file_signatures> ListFileSignatures(Loan_Request_File_Path_VM FileDetail);

        Task<bool> ApproveLoanRequest(Loan_Request_VM Data, string Comments);
        Task<bool> RejectLoanRequest(Loan_Request_VM Data, string Comments, VMDocHanlderDetails ForwardEmp);
        Task<bool> RecommendLoanRequest(Loan_Request_VM Data, string Comments, VMDocHanlderDetails ForwardEmp, StatusEnum.RecommendationStatusType RecommendStatus);
        Task<bool> SkipLoanRequest(Loan_Request_VM Data, string Comments, VMDocHanlderDetails ForwardEmp);
        Task<bool> ReturnLoanRequest(Loan_Request_VM Data, string Comments, VMDocHanlderDetails ForwardEmp);
        Task<List<Loan_Request_Recommenders_VM>> GetAllRecommendersbyLoanId(int loanId);
        void ChangeRecommendersbyLoanId(List<Loan_Request_Recommenders_VM> RecommendersVMList);
        Task<bool> ChangeRecommenders(VMChangeRecommenders Data);
        Task<List<Loan_Request_VM>> GetTop20LoanDetail(int empId, string status);
        string DownloadFileForInitiator(int fileId, Loan_Request_File_Path FileDetail);


        Task<List<Loan_Request_VM>> GetAllByDate(DateTime StartDate, DateTime EndDate, int? Initiator = null);
    }
}
