using AutoMapper;
using DMS.DAL.DatabaseContext;
using DMS.DAL.EntityModels;
using DMS.DAL.Repositories.GenericRepo;
using DMS.Services.General.Interface;
using DMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMS.Services.General.Implementation
{
    public class RolePermissionServices : CFGenericRepo<AspNetRoleMenuItem, int>, IRolePermissionServices
    {
        public void CreateMenuListbyEmpType(string Emp_Type, int[] Perm1)
        {
            List<AspNetRoleMenuItem> EmployeeType_MenuList_List = new List<AspNetRoleMenuItem>();
            List<AspNetRoleMenuItem> MenuListByEmpType = _db.AspNetRoleMenuItems.ToList();
            MenuListByEmpType = MenuListByEmpType.Where(x => x.RoleId == Emp_Type).ToList();
            if (MenuListByEmpType.Any())
            {
                foreach (var item in MenuListByEmpType)
                {
                    DeleteData(item.Id);
                }

            }
            foreach (var item in Perm1)
            {
                AspNetRoleMenuItem EmployeeType_MenuList = new AspNetRoleMenuItem();

                EmployeeType_MenuList.RoleId = Emp_Type;
                EmployeeType_MenuList.MenuListId = item;
                AddWithOutOtherField(EmployeeType_MenuList);
            }

        }
        public List<AspNetRoleMenuItem> GetApplicationUserMenuList()
        {
            List<AspNetRoleMenuItem> EmployeeType_MenuList_List = GetAllSync();
            //List<AspNetRoleMenuItem> EmployeeType_MenuListVMList = Mapper.Map<List<AspNetRoleMenuItem>>(EmployeeType_MenuList_List);
            return EmployeeType_MenuList_List;
        }

    }
}