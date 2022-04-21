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
    public interface IFeeCollectedServices:IGenericRepo<cad_fee_collected,int>
    {
        IList<cad_fee_collectedVM> GetFeeCollectionDetailsByCadId(int Id);
    }
}
