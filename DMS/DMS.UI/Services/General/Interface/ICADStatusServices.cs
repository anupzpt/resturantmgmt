using DMS.DAL.DatabaseContext;
using DMS.DAL.Repositories.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DMS.Services.General.Interface
{
    public interface ICADStatusServices:IGenericRepo<cad_status,int>
    {
        Task<IEnumerable<SelectListItem>> GetStatusDropDown(int Id);
    }
}
