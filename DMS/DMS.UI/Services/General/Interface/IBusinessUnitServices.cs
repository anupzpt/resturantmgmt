using DMS.DAL.DatabaseContext;
using DMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Services.General.Interface
{
    public interface IBusinessUnitServices
    {
        Task<List<Business_Unit_VM>> GetAllBusinessUnit();

        Task<List<Business_Unit>> GetAllValidBusinessUnit();

        Task<List<Business_Unit_VM>> GetAllValidBusinessUnitVM();
        

        void AddBusinessUnit(Business_Unit_VM business_Unit_VM);

        void DeleteBusinessUnit(int id);

        void EditBusinessUnit(Business_Unit_VM business_Unit_VM);

        Task<Business_Unit_VM> GetBusinessUnitbyId(int id);
    }
}