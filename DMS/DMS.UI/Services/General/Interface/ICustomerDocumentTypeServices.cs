using DMS.DAL.DatabaseContext;
using DMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Services.General.Interface
{
    public interface ICustomerDocumentTypeServices
    {
        Task<List<Customer_Document_Type_VM>> GetAllCustomerDocumentType();

        Task<List<Customer_Document_Type>> GetAllValidCustomerDocumentType();
        Task<List<Customer_Document_Type_VM>> GetAllValidCustomerDocumentTypeVM(); 

        void AddCustomerDocumentType(Customer_Document_Type_VM customer_Document_Type_VM);

        void DeleteCustomerDocumentType(int id);
        Customer_Document_Type GetCustomerDocumentTypeSyncbyId(int id);

        void EditCustomerDocumentType(Customer_Document_Type_VM customer_Document_Type_VM);

        Task<Customer_Document_Type_VM> GetCustomerDocumentTypebyId(int id);
    }
}