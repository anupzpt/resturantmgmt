using DMS.DAL.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Repositories.GeneralRepo.Interfaces
{
    public interface IUserCodesRepo : BaseModel.IGeneralRepositories<UserCode, long>
    {
    }
}
