using DMS.DAL.DatabaseContext;
using DMS.DAL.Repositories.GenericRepo;
using DMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DMS.Services.General.Interface
{
    public interface IDocHandlersService : IGenericRepo<DocHandler,int>
    {
        Task<VMDocHanlderDetails> GetDetails(int id);
        VMDocHanlderDetails GetDetailsSync(int id);
        Task<IEnumerable<VMDocHanlderDetails>> GetLoanRecommenderList();
        Task<IEnumerable<VMDocHanlderDetails>> GetLoanApproverList();
        Task<VMDocHanlderDetails> GetByEmployeeID(int EmpId);
    }
}
