using DMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Services.General.Interface
{
    public interface IDesignationServices
    {
        Task<List<Designation_VM>> GetAllDesignation();
        Task<List<Designation_VM>> GetAllValidDesignationVM();
        void AddDesignation(Designation_VM Designation_VM);
        void DeleteDesignation(int id);
        void EditDesignation(Designation_VM Designation_VM);
        Task<Designation_VM> GetDesignationbyId(int id);
    }
}
