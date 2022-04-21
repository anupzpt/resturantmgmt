using DMS.DAL.DatabaseContext;
using DMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Services.General.Interface
{
    public interface IDocumentPhaseServices
    {
        Task<List<Document_Phase_VM>> GetAllDocumentPhase();

        Task<List<Document_Phase>> GetAllValidDocumentPhase();
        Task<List<Document_Phase_VM>> GetAllValidDocumentPhaseVM();

        void AddDocumentPhase(Document_Phase_VM document_Phase_VM);

        void DeleteDocumentPhase(int id);

        void EditDocumentPhase(Document_Phase_VM document_Phase_VM);

        Task<Document_Phase_VM> GetDocumentPhasebyId(int id);
    }
}