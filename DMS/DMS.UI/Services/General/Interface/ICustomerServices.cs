using DMS.DAL.DatabaseContext;
using DMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Services.General.Interface
{
    public interface ICustomerServices
    {
        Task<List<Customer_VM>> GetAllCustomerVM();
        Task<List<Customer>> GetAllValidCustomer();

        Task<List<Customer_VM>> GetAllValidCustomerVM();
        void AddCustomer(Customer_VM Customer_VM);
        void DeleteCustomer(int id);
        void EditCustomer(Customer_VM Customer_VM);
        Customer_VM GetCustomerSyncbyId(int id);
        Task<Customer_VM> GetCustomerbyId(int id);
        List<Customer_VM> GetCustomerListbyCustomerType(string customerType);
        Task<List<Customer_VM>> Last10Customers(int Top = 10);
    }
}
