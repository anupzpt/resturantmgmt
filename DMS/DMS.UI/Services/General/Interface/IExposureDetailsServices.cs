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
    public interface IExposureDetailsServices:IGenericRepo<Cad_ExposureDetails,int>
    {
        IList<Cad_ExposureDetailsVM> GetExposureDetailsByCadId(int Id);
    }
}
