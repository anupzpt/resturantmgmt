using DMS.DAL.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Interface
{
    public interface ILevelsRepo : BaseModel.IGeneralRepositories<lvl01employee_levels, int>
    {
        
    }
}
