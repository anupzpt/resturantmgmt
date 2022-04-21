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
    public interface IBranchServices
    {
        Task<List<Branch_VM>> GetAllBranchVM();
        Task<List<Branch_VM>> GetAllValidBranchVM();
        Task<List<Branch>> GetAllBranch();
        Task<List<Branch>> GetAllValidBranch();
        void AddBranch(Branch_VM BranchVM);
        void DeleteBranch(int id);
        void EditBranch(Branch_VM BranchVM);
        Task<Branch_VM> GetBranchbyId(int id);
        Task<IEnumerable<SelectListItem>> GetBranchDropDown(int Id);

    }
}
