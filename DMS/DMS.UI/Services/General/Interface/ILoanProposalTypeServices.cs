using DMS.DAL.DatabaseContext;
using DMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Services.General.Interface
{
    public interface ILoanProposalTypeServices
    {
        Task<List<Loan_Proposal_Type_VM>> GetAllLoanProposalType();

        Task<List<Loan_Proposal_Type>> GetAllValidLoanProposalType();

        void AddLoanProposalType(Loan_Proposal_Type_VM loan_Proposal_Type_VM);

        void DeleteLoanProposalType(int id);

        void EditLoanProposalType(Loan_Proposal_Type_VM loan_Proposal_Type_VM);

        Task<Loan_Proposal_Type_VM> GetLoanProposalTypebyId(int id);
    }
}