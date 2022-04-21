using DMS.DAL.DatabaseContext;
using DMS.DAL.Repositories.GenericRepo;
using DMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Services.General.Interface
{
    public interface IDocumentCollectedServices:IGenericRepo<cad_documents_collected,int>
    {
        IList<cad_documents_collectedVM> GetDocumentCollectionDetailsByCadId(int Id);
    }
}
