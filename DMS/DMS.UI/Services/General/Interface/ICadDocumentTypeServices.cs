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
    public interface ICadDocumentTypeServices
    {
        Task<List<Cad_Document_Type_VM>> GetAllCadDocumentType();

        Task<List<Cad_Document_Type>> GetAllValidCadDocumentType();

        Task<List<Cad_Document_Type_VM>> GetAllValidCadDocumentTypeVM(); 

        void AddCadDocumentType(Cad_Document_Type_VM cad_Document_Type_VM);

        void DeleteCadDocumentType(int id);

        void EditCadDocumentType(Cad_Document_Type_VM cad_Document_Type_VM);

        Task<Cad_Document_Type_VM> GetCadDocumentTypebyId(int id);
        Task<IEnumerable<SelectListItem>> GetDocumentTypeDropdown(int Id);

    }
}