using DMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DMS.Services.General.Interface
{
    public interface ICadFeeTypeServices
    {
        Task<List<Cad_Fee_TypeVM>> GetAllCadFeeType();
        Task<List<Cad_Fee_TypeVM>> GetAllCadFeeTypeVM();

        Task<List<Cad_Fee_TypeVM>> GetAllValidCadFeeTypeVM();

        void AddCadFeeType(Cad_Fee_TypeVM Cad_Fee_TypeVM);

        void DeleteCadFeeType(int id);

        void EditCadFeeType(Cad_Fee_TypeVM Cad_Fee_TypeVM);

        Task<Cad_Fee_TypeVM> GetCadFeeTypebyId(int id);

        Task<IEnumerable<SelectListItem>> GetFeesDropdown(int Id);
    }
}