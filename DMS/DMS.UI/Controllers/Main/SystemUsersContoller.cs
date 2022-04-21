using DMS.CustomAttributes;
using DMS.DAL;
using DMS.DAL.DatabaseContext;
using DMS.DAL.EntityModels;
using DMS.DAL.Helpers;
using DMS.DAL.Interface;
using DMS.DAL.Interfaces;
using DMS.DAL.Repositories.GeneralRepo.Implementation;
using DMS.DAL.Repositories.GeneralRepo.Interfaces;
using DMS.DAL.Repositories.MainRepo;
using DMS.DAL.StaticHelper;
using DMS.Helpers;
using DMS.Models;
using DMS.Services;
using DMS.UI.Services.General.Interface;
using DMS.UI.ViewModels;
using DMS.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DMS.UI.Controllers
{
    [AllowTopUsers]
    [CustomAuthentication]
    public class SystemUsersController : AccountController
    {
        private IUserRoleRepo _UserManagement;
        private IBranchesRepo _BranchesRepo;
        private IEmployeeRepo _EmployeeRepo;
        //protected ApplicationUserManager _userManager;
        protected IUserRoleServices _UserRoleServices;
        //protected RegisterViewModel Model;
        protected IUserRepo _UserRepo;
        protected EmailHelperService _EmailHelperService;
        protected IUserCodesRepo _UserCodesRepo;
        IDesignationRepo _DesignationRepo;
        IDepartmentRepo _DepartmentRepo;
        ILevelsRepo _LevelsRepo;

        public SystemUsersController(
            MainEntities MainEntities,
            ApplicationUserManager userManager,
            ApplicationSignInManager signInManager,
            IUserRoleServices userRole,
            IBranchesRepo BranchesRepo,
            IUserRepo User05Repo,
            IEmployeeRepo EmployeeRepo,
            EmailHelperService EmailHelperService,
            IUserCodesRepo UserCodesRepo,
            IDesignationRepo DesignationRepo,
            IDepartmentRepo DepartmentRepo,
            ILevelsRepo LevelsRepo
            ) : base(MainEntities, userManager, signInManager, userRole, EmailHelperService,UserCodesRepo,EmployeeRepo, User05Repo)
        {
            _UserManagement = new UserRoleRepo();
            _BranchesRepo = BranchesRepo;
            //_userManager = userManager;
            _UserRepo = User05Repo;
            _EmployeeRepo = EmployeeRepo;
            _EmailHelperService = EmailHelperService;
            _UserCodesRepo = UserCodesRepo;
            _DesignationRepo = DesignationRepo;
            _DepartmentRepo = DepartmentRepo;
            _LevelsRepo = LevelsRepo;
        }
        public ActionResult Index()
        {
            IEnumerable<ApplicationUserVM> UserList = _userRole.GetAllUserList().ToList();//.Where(x => x.usr05deleted == false).ToList();
            IEnumerable<usr05users> User05List = _UserRepo.GetList().Include(x => x.bra01branches).Include(x => x.emp01employee).ToList();

            IList<UserModule> UserModelList = (from ApplicationUserVM in UserList
                                               join usr05users in User05List
                                               on ApplicationUserVM.Id equals usr05users.usr05userId into gj
                                               from subpet in gj.DefaultIfEmpty()
                                               select new UserModule() { ApplicationUser = ApplicationUserVM, UserAgent = subpet }
                                              )/*.Where(x => x.UserAgent.usr05deleted == false)*/.ToList();

            return View(UserModelList);
        }

        public override void InitCommon(RegisterViewModel Model)
        {
            base.InitCommon(Model);
            //code for dropdown of agent
            ViewBag.BranchId = new SelectList(_dbMainEntities.bra01branches.Where(x => x.bra01deleted == false).ToList(), "bra01uin", "DisplayField", Model.BranchId);
            ViewBag.EmployeeId = new SelectList(_dbMainEntities.emp01employee.Where(x => x.emp01deleted == false && x.usr05users.Count == 0).ToList(), "emp01uin", "DisplayField", Model.EmployeeId);
            ViewBag.ForEditEmployeeId = new SelectList(_dbMainEntities.emp01employee.Where(x => x.emp01deleted == false).ToList(), "emp01uin", "DisplayField", Model.EmployeeId);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override async Task<ActionResult> Register(RegisterViewModel Data)
        {
            try
            {
                if (Data.BranchId == null || Data.BranchId <= 0)
                {
                    throw new Exception("Branch ID Required.");
                }
                if (Data.EmployeeId == null || Data.EmployeeId <= 0)
                {
                    throw new Exception("Employee ID Required.");
                }
                var PermittedRoles = RoleList();

                if (Data.RegRoles.Any(x => !PermittedRoles.Any(y => y.Name == x)))
                {
                    throw new Exception("Permission Denied!!!. Cannot escalate user with higher permission.");
                }
                //PermittedRoles.Any(x=>x.Name == ))
                //check for duplicate employee id in users table
                var rec1 = _dbMainEntities.usr05users.Where(x => x.usr05emp01uin == Data.EmployeeId).FirstOrDefault();
                if (rec1 != null)
                {
                    throw new Exception("Cannot assign same user for 2 diff usernames.");
                }

                Data.Password = PasswordHelper.GetAutoGeneratedPassword();
                Data.ConfirmPassword = Data.Password;
                ModelState.Clear();
                TryValidateModel(Data);
                return await base.Register(Data);
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        ModelState.AddModelError("", string.Format("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage));
                    }

                    // Rollback changes
                    DbEntityEntry entry = validationErrors.Entry;
                    string entityTypeName = entry.Entity.GetType().Name;

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ErrorHelper.GetMsg(ex));
            }
            InitCommon(Data);
            return View();
        }

        public override ActionResult RegisterUserEmployee()
        {
            return base.RegisterUserEmployee();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterUserEmployee(RegisterViewModel Data)
        {
            try
            {
                if (Data.BranchId == null || Data.BranchId <= 0)
                {
                    throw new Exception("Branch ID Required.");
                }
                if (Data.Email == null)
                {
                    throw new Exception("Employee Email ID Required.");
                }
                var PermittedRoles = RoleList();

                if (Data.RegRoles.Any(x => !PermittedRoles.Any(y => y.Name == x)))
                {
                    throw new Exception("Permission Denied!!!. Cannot escalate user with higher permission.");
                }
                var dep = _DepartmentRepo.GetList();
                if(dep.Count()==0)
                {
                    throw new Exception("Please Setup at least one Department");
                }
                var des = _DesignationRepo.GetList();
                if(des.Count()==0)
                {
                    throw new Exception("Please Setup at least one Designation");
                }
                var lvl = _LevelsRepo.GetList();
                if(lvl.Count()==0)
                {
                    throw new Exception("Please Setup at least one Level");
                }
                emp01employee emp01employee = new emp01employee();
                emp01employee.emp01created_by = _ActiveSession.UserId;
                emp01employee.emp01created_date_eng = _ActiveSession.SysDateEng;
                emp01employee.emp01created_date_nep = _ActiveSession.SysDateNep;
                emp01employee.emp01deleted = false;
                emp01employee.emp01updated_by = _ActiveSession.UserId;
                emp01employee.emp01join_date_nep = _ActiveSession.SysDateNep;
                emp01employee.emp01updated_date_nep = _ActiveSession.SysDateNep;
                emp01employee.emp01update_date_eng = _ActiveSession.SysDateEng;
                emp01employee.emp01bra01uin = Data.BranchId;
                emp01employee.emp01code = "E00" + Data.EmployeeName.Length;
                emp01employee.emp01dep01uin = dep.FirstOrDefault().dep01uin;
                emp01employee.emp01des01uin = des.FirstOrDefault().des01uin;
                emp01employee.emp01join_date_eng = _ActiveSession.SysDateEng;
                emp01employee.emp01join_date_nep = _ActiveSession.SysDateNep;
                emp01employee.emp01lvl01uin = lvl.FirstOrDefault().lvl01uin;
                emp01employee.emp01mobile = "1111111111";
                emp01employee.emp01name = Data.EmployeeName;
                emp01employee.emp01status = Data.status;
                emp01employee.emp01email = Data.Email;
                emp01employee.emp01address = "ktm";
                _EmployeeRepo.Insert(emp01employee);

                Data.EmployeeId = emp01employee.emp01uin;

                Data.Password = PasswordHelper.GetAutoGeneratedPassword();
                Data.ConfirmPassword = Data.Password;
                ModelState.Clear();
                TryValidateModel(Data);
                return await base.Register(Data);
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        ModelState.AddModelError("", string.Format("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage));
                    }

                    // Rollback changes
                    DbEntityEntry entry = validationErrors.Entry;
                    string entityTypeName = entry.Entity.GetType().Name;

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ErrorHelper.GetMsg(ex));
            }
            InitCommon(Data);
            return View();
        }
        private byte RegRoleTypebyte(RegisterViewModel userData)
        {
            if (userData.RegRoles.Count() <= 0)
            {
                throw new Exception("At least one user role required.");
            }
            EnumUserRoles uRole = EnumHelper.Parse<EnumUserRoles>(userData.RegRoles.FirstOrDefault());

            byte ret = (byte)uRole;
            return ret;
        }
        public override bool AfterRegister(ApplicationUser RegUser, RegisterViewModel userData)
        {

            //loop user selectec roles
            //foreach (var item in userData.RegRoles)
            //{

            //    var Roles = (EnumUserRoles)Enum.Parse(typeof(EnumUserRoles), item);
            //    _UserManagement.AddUserToRoles(RegUser, Roles);
            //    if (Roles == EnumUserRoles.SuperAdmin)
            //    {
            //        if (userData.BranchId != null)
            //        {
            //            throw new Exception("Branch Id for SuperAdmin in not Required");

            //        }

            //    }
            //    if (Roles == EnumUserRoles.OrganizationAdmin)
            //    {
            //        if (userData.BranchId != null)
            //        {
            //            throw new Exception("Branch Id for Organization Admin in not Required");

            //        }
            //    }

            //    if (Roles == EnumUserRoles.BranchAdmin)
            //    {
            //        if (userData.BranchId == null)
            //        {
            //            throw new Exception("Branch is Required ");

            //        }

            //    }
            //}
            //  return true;
            //identity add user to selectec role

            //add user detail usr05.... table also
            //add user to selected user roles
            if (userData.BranchId != null)
            {
                usr05users User = new usr05users();

                User.usr05bra01uin = userData.BranchId;
                User.usr05userId = RegUser.Id;
                User.usr05status = true;
                User.usr05deleted = false;
                User.usr05emp01uin = userData.EmployeeId;
                User.usr05can_view_all_branch = userData.usr05can_view_all_branch;
                //User.usr05token = SaltEncription.SHA1HashStringForUTF8String(userData.Password);

                User.usr05type = 1;
                //User.usr05type = RegRoleTypebyte(userData);
                //1;//userData.RegRoles.;//to fdo enum for manager and operator//to remove

                User.usr05updated_by = _ActiveSession.UserId;
                User.usr05updated_date = _ActiveSession.SysDateEng;
                User.usr05created_by = _ActiveSession.UserId;
                User.usr05created_date = _ActiveSession.SysDateEng;
                //                db.usr05users.Add(User);
                _dbMainEntities.usr05users.Add(User);
                _dbMainEntities.SaveChanges();
                //_User05Repo.Save();
                userData.EmployeeId = User.usr05emp01uin.Value;
            }
            return true;
            // InitCommon(Model);

        }
        public async Task<ActionResult> Edit(string id)
        {
            RegisterViewModel RegisterViewModel = new RegisterViewModel();
            ViewBag.BranchId = new SelectList(_BranchesRepo.GetList().AsEnumerable(), "BindField", "DisplayField");

            RegisterEditModel data = new RegisterEditModel();
            ViewBag.BranchId = new SelectList(_BranchesRepo.GetList().AsEnumerable(), "BindField", "DisplayField");
            //ViewBag.EmployeeId = new SelectList(_EmployeeRepo.GetListByStatus().AsEnumerable().ToList(), "BindField", "DisplayField");

            usr05users user = _dbMainEntities.usr05users.Where(s => s.usr05userId == id).FirstOrDefault();

            var roleStore = new RoleStore<IdentityRole>(_dbIdentityEntities);
            var roleMngr = new RoleManager<IdentityRole>(roleStore);
            List<string> roleName = new List<string>();

            var xyz = _userManager.FindById(id);
            var NetUserRole = xyz.Roles.ToList();

            try
            {

                if (id == "")
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                }
                foreach (var item in xyz.Roles)
                {
                    var NetRoleinfo = roleMngr.FindById(item.RoleId);
                    roleName.Add(NetRoleinfo.Name);
                }


                var abc = _dbMainEntities.usr05users.Where(x => x.usr05userId == id).FirstOrDefault();

                data = new RegisterEditModel()
                {
                    BranchId = user == null ? 0 : user.bra01branches.bra01uin,
                    Email = xyz.Email,
                    RegRoles = roleName.ToArray(),
                    EmployeeId = user?.emp01employee == null ? 0 : user.emp01employee.emp01uin,
                    EmployeeName = user?.emp01employee == null ? "" : user.emp01employee.emp01name,
                    usr05can_view_all_branch = user.usr05can_view_all_branch,
                    status = user.usr05status,
                };

                if (data == null)
                {
                    return HttpNotFound();
                }
                RegisterViewModel.RegRoles = data.RegRoles;
                RegisterViewModel.BranchId = data.BranchId.Value;
                RegisterViewModel.EmployeeId = data.EmployeeId.Value;

                // InitCommon(RegisterViewModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            InitCommon(RegisterViewModel);
            return View(data);
        }


        [HttpPost]
        public virtual async Task<ActionResult> Edit(RegisterEditModel model)
        {
            RegisterViewModel RegisterViewModel = new RegisterViewModel();

            if (ModelState.IsValid)
            {
                //if (model.Password == null || model.Password == "")
                //{
                //    throw new Exception("invalid password");
                //}
                //if (model.ConfirmPassword == null || model.ConfirmPassword == ""
                //   )
                //{
                //    throw new Exception("Invalid Conformed  Password");
                //}
                if (model.Password != model.ConfirmPassword
                    )
                {
                    throw new Exception("Password MisMatched");
                }
                ApplicationUser UserData;
                try
                {
                    UserData = await _userManager.FindByEmailAsync(model.Email);

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                if (UserData == null)
                {
                    throw new Exception("Cannot find Email-id");
                }

                UserData.IsActive = model.status;

                usr05users Data = await _dbMainEntities.usr05users.Where(s => s.usr05userId == UserData.Id).FirstOrDefaultAsync();

                // Get user roles
                string[] allUserRoles = _userManager.GetRoles(UserData.Id).ToArray();
                IdentityResult r1 = _userManager.RemoveFromRoles(UserData.Id, allUserRoles);
                //_userManager.RemoveFromRole(UserData.Id, item.RoleId.ToString());
                if (!r1.Succeeded)
                {
                    throw new Exception(string.Join(",", r1.Errors));
                }

                _userManager.AddToRoles(UserData.Id, model.RegRoles);

                if (Data != null)
                {
                    Data.usr05bra01uin = model.BranchId.Value;
                    Data.usr05can_view_all_branch = model.usr05can_view_all_branch;
                    Data.usr05status = model.status;
                    Data.usr05deleted = false;
                    //Data.usr05emp01uin = model.EmployeeId.Value;
                    //update and save database
                    _dbMainEntities.Entry(Data).State = EntityState.Modified;
                    _dbMainEntities.SaveChanges();
                }
                else
                {
                    //create new associative user if it has employee id
                    if (model.EmployeeId > 0)
                    {
                        //validation check if this selected emp id has been assigned to others or not
                        //thorw error if yes
                        Data = new usr05users()
                        {
                            usr05emp01uin = model.EmployeeId,
                            usr05userId = UserData.Id,
                            usr05bra01uin = model.BranchId.Value,
                            usr05created_by = _ActiveSession.UserId,
                            usr05created_date = _ActiveSession.SysDateEng,
                            usr05deleted = false,
                            usr05status = true,
                            usr05updated_by = _ActiveSession.UserId,
                            usr05updated_date = _ActiveSession.SysDateEng,
                            usr05type = 1,
                            usr05can_view_all_branch = model.usr05can_view_all_branch,
                        };
                        _dbMainEntities.usr05users.Add(Data);
                        _dbMainEntities.SaveChanges();
                    }
                }

                FlashBag.setMessage(true, "Successfully Updated");

                if (model.Password != null && model.Password.Trim().Length > 0)
                {
                    var stat = await _userManager.ChangePasswordAdmin(UserData.Id, model.Password);
                    if (stat.Succeeded == false)
                    {
                        FlashBag.setMessage(false, string.Join(",", stat.Errors.ToArray()));
                    }
                }

                InitCommon(RegisterViewModel);

                return RedirectToAction("Index");
            }
            // If we got this far, something failed, redisplay form
            InitCommon(RegisterViewModel);
            return View(model);
        }



        public ActionResult Delete(string id)
        {

            RegisterViewModel RegisterViewModel = new RegisterViewModel();
            ViewBag.BranchId = new SelectList(_BranchesRepo.GetList().AsEnumerable(), "BindField", "DisplayField");
            // ViewBag.EmployeeId = new SelectList(_EmployeeRepo.GetList().AsEnumerable().ToList(), "BindField", "DisplayField");
            RegisterEditModel data = new RegisterEditModel();
            ViewBag.BranchId = new SelectList(_BranchesRepo.GetList().AsEnumerable(), "BindField", "DisplayField");
            // ViewBag.EmployeeId = new SelectList(_EmployeeRepo.GetList().AsEnumerable().ToList(), "BindField", "DisplayField");
            usr05users user = _dbMainEntities.usr05users.Where(s => s.usr05userId == id).FirstOrDefault();
            var roleStore = new RoleStore<IdentityRole>(_dbIdentityEntities);
            var roleMngr = new RoleManager<IdentityRole>(roleStore);
            List<string> roleName = new List<string>();

            var xyz = _userManager.FindById(id);
            var NetUserRole = xyz.Roles.ToList();

            try
            {

                if (id == "")
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                }
                foreach (var item in xyz.Roles)
                {
                    var NetRoleinfo = roleMngr.FindById(item.RoleId);
                    roleName.Add(NetRoleinfo.Name);
                }


                var abc = _dbIdentityEntities.Users.Where(x => x.Id == id).FirstOrDefault();

                data = new RegisterEditModel()
                {
                    BranchId = user == null ? 0 : user.bra01branches.bra01uin,
                    Email = xyz.Email,
                    RegRoles = roleName.ToArray(),


                };

                if (data == null)
                {
                    return HttpNotFound();
                }
                RegisterViewModel.RegRoles = data.RegRoles;
                RegisterViewModel.BranchId = data.BranchId.Value;

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            InitCommon(RegisterViewModel);
            return View(data);
        }


        [HttpPost]
        public async Task<ActionResult> Delete(RegisterEditModel model)
        {
            RegisterViewModel RegisterViewModel = new RegisterViewModel();



            ApplicationUser UserData = await _userManager.FindByEmailAsync(model.Email);
            if (UserData == null)
            {
                throw new Exception("Cannot find Email-id");
            }
            usr05users Data = await _dbMainEntities.usr05users.Where(s => s.usr05userId == UserData.Id).FirstOrDefaultAsync();

            IList<string> userRoles = _userManager.GetRoles(UserData.Id);


            foreach (var item in UserData.Roles)
            {

                _userManager.RemoveFromRole(item.UserId, item.RoleId);

            }
            //   _userManager.RemoveFromRole(UserData.Id, model.RegRoles);

            if (Data != null)
            {
                // Data.usr05bra01uin = model.BranchId.Value;
                //update and save database
                Data.usr05deleted = true;
                _dbMainEntities.Entry(Data).State = EntityState.Modified;
                _dbMainEntities.SaveChanges();
                FlashBag.setMessage(true, "Successfully Updated");

                InitCommon(RegisterViewModel);
                return RedirectToAction("Index");
            }



            //get user's assigned roles


            //check for role to be removed


            // If we got this far, something failed, redisplay form
            InitCommon(RegisterViewModel);
            return View(model);
        }

        public async Task<ActionResult> Details(string id)
        {
            RegisterViewModel data = new RegisterViewModel();
            ViewBag.BranchId = new SelectList(_BranchesRepo.GetList().AsEnumerable(), "BindField", "DisplayField");

            usr05users user = _dbMainEntities.usr05users.Where(s => s.usr05userId == id).FirstOrDefault();
            var roleStore = new RoleStore<IdentityRole>(_dbIdentityEntities);
            var roleMngr = new RoleManager<IdentityRole>(roleStore);
            List<string> roleName = new List<string>();

            var xyz = _userManager.FindById(id);
            var NetUserRole = xyz.Roles.ToList();

            try
            {

                if (id == "")
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                }
                foreach (var item in xyz.Roles)
                {
                    var NetRoleinfo = roleMngr.FindById(item.RoleId);
                    roleName.Add(NetRoleinfo.Name);
                }


                var abc = _dbIdentityEntities.Users.Where(x => x.Id == id).FirstOrDefault();
                var nerole =
                data = new RegisterViewModel()
                {
                    BranchName = user.bra01branches.bra01name,
                    Email = xyz.Email,
                    RegRoles = roleName.ToArray(),
                    EmployeeId = user?.emp01employee == null ? 0 : user.emp01employee.emp01uin,
                    EmployeeName = user?.emp01employee == null ? "" : user.emp01employee.emp01name,

                };

                if (data == null)
                {
                    return HttpNotFound();
                }
                InitCommon(data);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(data);
        }

        public async Task<ActionResult> MyProfile()
        {
            SystemInfoForSession Data = _ActiveSession;
            //IEnumerable<abc03abc> List = Repo.GetList();
            ViewBag.Conn1 = new VMConnectionInfo()
            {
                ServerName = _dbIdentityEntities.Database.Connection.DataSource,
                DatabaseName = _dbIdentityEntities.Database.Connection.Database,
                UserName = _dbIdentityEntities.Database.Connection.State.ToString()
            };
            ViewBag.Conn2 = new VMConnectionInfo()
            {
                ServerName = _dbMainEntities.Database.Connection.DataSource,
                DatabaseName = _dbMainEntities.Database.Connection.Database,
                UserName = _dbMainEntities.Database.Connection.ConnectionString
            };
            return View(Data);
        }

    }
}