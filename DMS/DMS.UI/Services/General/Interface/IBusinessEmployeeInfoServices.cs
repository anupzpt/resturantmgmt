using DMS.DAL.DatabaseContext;
using DMS.DAL.Repositories.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Services.General.Interface
{
    public interface IBusinessEmployeeInfoServices:IGenericRepo<Business_Employee_Info,int>
    {

    }
}
