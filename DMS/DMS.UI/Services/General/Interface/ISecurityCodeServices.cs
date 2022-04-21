using DMS.DAL.DatabaseContext;
using DMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DMS.Services.General.Interface
{
    public interface ISecurityCodeServices
    {
        Task<List<Security_Code_VM>> GetAllSecurityCode();

        Task<List<Security_Code>> GetAllValidSecurityCode();

        void AddSecurityCode(Security_Code_VM security_Code_VM);

        void DeleteSecurityCode(int id);

        void EditSecurityCode(Security_Code_VM security_Code_VM);

        Task<Security_Code_VM> GetSecurityCodebyId(int id);
        Task<IEnumerable<SelectListItem>> GetSecurityDropDown(int Id);

    }
}