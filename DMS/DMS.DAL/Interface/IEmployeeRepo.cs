using DMS.DAL.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Interfaces
{
    public interface IEmployeeRepo : BaseModel.IGeneralRepositories<emp01employee, int>
    {
        
    }
}
