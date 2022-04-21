using DMS.DAL.DatabaseContext;
using DMS.DAL.EntityModels;
using DMS.DAL.Repositories.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Repositories.GeneralRepo.Interfaces
{
    public interface IControllerActionRepo : IGenericRepo<ControllerAction, int>
    {
    }
}
