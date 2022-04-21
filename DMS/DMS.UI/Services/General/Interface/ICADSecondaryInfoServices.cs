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
    public interface ICADSecondaryInfoServices:IGenericRepo<Cad_Secondary_Information,int>
    {
        void UpdateCADInformation(Cad_Secondary_InformationVM cadinfo);
        Cad_Secondary_InformationVM GetFileIconPath(Cad_Secondary_InformationVM cadsecinfo);
    }   
}
