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
    public interface IDistrictServices
    {
        Task<List<District_VM>> GetAllDistict();
        Task<List<District_VM>> GetAllValidDistictVM();
        void AddDistrict(District_VM DistrictVM);
        void DeleteDistrict(int id);
        void EditDistrict(District_VM DistrictVM);
        Task<District_VM> GetDistrictbyId(int id);
        

    }
}