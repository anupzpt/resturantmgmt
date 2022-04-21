using DMS.DAL.DatabaseContext;
using DMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Services.General.Interface
{
    public interface IDocumentTypeServices
    {
        Task<List<Document_Type_VM>> GetAllDocumentType();

        Task<List<Document_Type>> GetAllValidDocumentType();

        Document_Type GetAllValidDocumentTypeByIdSync(int id);
        Task<List<Document_Type_VM>> GetAllValidDocumentTypeVM();

        void AddDocumentType(Document_Type_VM document_Type_VM);

        void DeleteDocumentType(int id);

        void EditDocumentType(Document_Type_VM document_Type_VM);

        Task<Document_Type_VM> GetDocumentTypebyId(int id);
    }
}