using DMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Services.General.Interface
{
    public interface IBlackListCustomerServices
    {

        List<Customer_BlackList_VM> GetAllBlackListVM();
        void AddBlackListCustomer(Customer_BlackList_VM Customer_BlackList_VM);
        void EditBlackListedCustomer(Customer_BlackList_VM Customer_BlackList_VM);
        Customer_BlackList_VM GetBlackListById(int id);
        Customer_BlackList_VM GetBlackListbyName(string name);
    }
}
