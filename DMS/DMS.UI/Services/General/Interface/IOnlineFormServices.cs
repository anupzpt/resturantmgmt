using DMS.DAL.DatabaseContext;
using DMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Services.General.Interface
{
    public interface IOnlineFormServices
    {
        OnlineForm_Customer CreateCustomerAccount(OnlineForm_VM OnlineForm_VM);
        OnlineForm_VM GetAccountByIdVM(int id);
        OnlineForm_Customer GetAccountById(int id);
        List<OnlineForm_VM> GetAllOnlineCustomerAccountVM();
        List<OnlineForm_Customer> GetAllOnlineCustomerAccount();
        bool UploadFiles(OnlineForm_VM OnlineForm_VM);

    }
}
