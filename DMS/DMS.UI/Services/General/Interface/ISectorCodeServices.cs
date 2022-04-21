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
    public interface ISectorCodeServices
    {
        Task<List<SectorCode_VM>> GetAllSector_Code();
        void AddSector_Code(SectorCode_VM Sector_CodeVM);
        void DeleteSector_Code(int id);
        void EditSector_Code(SectorCode_VM Sector_CodeVM);
        Task<SectorCode_VM> GetSector_CodebyId(int id);
        Task<IEnumerable<SelectListItem>> GetSectorDropDown(int Id);

    }
}
