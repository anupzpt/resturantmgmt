using AutoMapper;
using DMS.DAL.DatabaseContext;
using DMS.DAL.EntityModels;
using DMS.DAL.Helpers;
using DMS.Services.General.Interface;
using DMS.UI.Services.General.Interface;
using DMS.UI.ViewModels;
using DMS.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DMS.Controllers
{
    [CustomAuthentication]
    public class RolePermissionController : Controller
    {
        //private IEmployeeServices _employee;
        private IMenuServices _menuServices;
        private IRolePermissionServices _rolepermission;
        private IUserRoleServices _userRole;
        protected readonly IdentityEntities _db = new IdentityEntities();



        public RolePermissionController(IMenuServices menuServices, IRolePermissionServices rolepermission, IUserRoleServices UserRole)
        {
            //_employee = employee;
            _menuServices = menuServices;
            _rolepermission = rolepermission;
            _userRole = UserRole;
            //_DocHandlerService = DocHandlerService;
        }
        // GET: RolePermission

        [HttpGet]
        public ActionResult Index()
        {
            var roleStore = new RoleStore<IdentityRole>(_db);
            var roleMngr = new RoleManager<IdentityRole>(roleStore);

            var roles = roleMngr.Roles.ToList();
            return View(roles);
        }

        [HttpGet]
        public async Task<ActionResult> SetPermission(string Emp_Type)
        {
            Role_Permission_VM Role_Permission_VM = new Role_Permission_VM();
            Role_Permission_VM.ApplicationRole = await _userRole.GetById(Emp_Type);
            if (Role_Permission_VM.ApplicationRole == null)
            {
                throw new Exception("User Role Id Not Found");
            }

            //_employee.GetEmployeeTypeList().Where(x => x.Emp_Type == Emp_Type).FirstOrDefault();
            List<AspNetRoleMenuItem> AlredySelectedMenuItems = _rolepermission.GetApplicationUserMenuList().Where(x => x.RoleId == Emp_Type).ToList();

            List<MenuListVM> AllMenuItems = Mapper.Map<List<MenuListVM>>(await _menuServices.GetAll());
            List<MenuListVM> SelectedMenuListVMList = AllMenuItems.Where(x => AlredySelectedMenuItems.Any(y => y.MenuListId == x.Id)).ToList();
            List<MenuListVM> NotSelectedMenuListVMList = AllMenuItems.Where(x => !SelectedMenuListVMList.Any(y => y.Id == x.Id)).ToList();
            Role_Permission_VM.SelectedMenuListVMList = SelectedMenuListVMList;
            Role_Permission_VM.NotSelectedMenuListVMList = NotSelectedMenuListVMList;
            return View(Role_Permission_VM);
        }

        [HttpPost]
        public ActionResult SetPermission(string Emp_Type, int[] Perm1)
        {
            try
            {
                _rolepermission.CreateMenuListbyEmpType(Emp_Type, Perm1);
                TempData["Message"] = "Permission has been given Successfully.";

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> UserList(string id)
        {
            //check conversion
            ApplicationRoleVM List = _userRole.GetUserListWithRole(id);
            //EnumEmployeeRoles uData = EnumEmployeeRoles.LoanInitiator;
            //Enum.TryParse(id, out uData);
            //ViewBag.EmpType = uData.ToString();
            //IEnumerable<VMDocHanlderDetails> List1 = await _DocHandlerService.GetListByRole(uData);
            return View(List);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(string RoleName)
        {
            try
            {
                if (RoleName.Trim().Length <= 0)
                {
                    throw new Exception("Role Name Required.");
                }
                _userRole.AddRole(RoleName);
                FlashBag.setMessage(true, "New Role Created Successfully. " + RoleName);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ErrorHelper.GetMsg(ex));
            }
            return View();
        }
    }
}