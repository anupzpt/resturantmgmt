using DMS.DAL.EntityModels;
using DMS.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DMS.ViewModels
{

    public class Role_Permission_VM
    {
        public Role_Permission_VM()
        {
            ApplicationRole = new ApplicationRole();
            MenuListVMList = new List<MenuListVM>();
            SelectedMenuListVMList = new List<MenuListVM>();
            EmployeeType_MenuListVMList = new List<EmployeeType_MenuListVM>();
            NotSelectedMenuListVMList = new List<MenuListVM>();
        }
        public bool IsSelected { get; set; }
        public ApplicationRole ApplicationRole { get; set; }
        public List<MenuListVM> SelectedMenuListVMList { get; set; }
        public List<MenuListVM> NotSelectedMenuListVMList { get; set; }
        public List<MenuListVM> MenuListVMList { get; set; }
        public List<EmployeeType_MenuListVM> EmployeeType_MenuListVMList { get; set; }

    }
    public class EmployeeType_MenuListVM
    {

        public int Id { get; set; }
        [Display(Name = "User Role")]
        public string ApplicationRole { get; set; }
        public int MenuId { get; set; }
    }


}