using DMS.DAL.Helpers;
using DMS.DAL.Repositories.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Repositories.BaseRepo
{
    public abstract class AuthGeneralRepo<EntytType, PK> : GenericRepo<EntytType, PK>
        where EntytType : class
    {
        public readonly AuthHelper _Auth;
        public AuthGeneralRepo(AuthHelper Auth)
        {
            _Auth = Auth;
        }
    }
}
