using DMS.DAL.DatabaseContext;
using DMS.DAL.Repositories.GeneralRepo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Repositories.GeneralRepo.Implementation
{
    public class UserCodesRepo :
        BaseModel._AbsGeneralRepositories<MainEntities, UserCode, long>,
        IUserCodesRepo
    {
        public UserCodesRepo(MainEntities context) : base(context)
        {
        }
    }
}
