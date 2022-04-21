using DMS.CustomAttributes;
using DMS.DAL;
using DMS.DAL.DatabaseContext;
using DMS.DAL.EntityModels;
using DMS.DAL.Helpers;
using DMS.DAL.Interfaces;
using DMS.DAL.Repositories.GeneralRepo.Interfaces;
using DMS.DAL.StaticHelper;
using DMS.Helpers;
using DMS.Models;
using DMS.Services;
using DMS.UI.Services.General.Implementation;
using DMS.UI.Services.General.Interface;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DMS.UI.Controllers
{
    [CustomAuthentication]
    public class AccountController : Controller
    {
        protected readonly ApplicationSignInManager _signInManager;
        protected readonly ApplicationUserManager _userManager;
        protected readonly IUserRoleServices _userRole;
        protected readonly SystemInfoForSession _ActiveSession;
        protected readonly IdentityEntities _dbIdentityEntities = new IdentityEntities();
        protected readonly MainEntities _dbMainEntities;
        protected EmailHelperService _EmailHelperService;
        private IUserCodesRepo _UserCodesRepo;
        private IEmployeeRepo _EmployeeRepo;
        private IUserRepo _UserRepo;

        public AccountController(
            MainEntities dbCore,
            ApplicationUserManager userManager,
            ApplicationSignInManager signInManager,
            IUserRoleServices userRole, /*ICompanyInfoServices companyInfo, IMasterSettingServices masterSetting*/
            EmailHelperService EmailHelperService,
            IUserCodesRepo UserCodesRepo,
            IEmployeeRepo EmployeeRepo,
            IUserRepo UserRepo)
        {
            _dbMainEntities = dbCore;
            _userRole = userRole;
            _userManager = userManager;
            _signInManager = signInManager;
            _EmailHelperService = EmailHelperService;
            _ActiveSession = SessionHelper.GetSession();
            _UserCodesRepo = UserCodesRepo;
            _EmployeeRepo = EmployeeRepo;
            _UserRepo = UserRepo;
        }
        public virtual void InitCommon(RegisterViewModel Model)
        {
            //var roleStore = new RoleStore<IdentityRole>(_db);
            //var roleMngr = new RoleManager<IdentityRole>(roleStore);
            //var roles = roleMngr.Roles.ToList();
            //code for dropdown of aspnetroles
            var abc = this.RoleList();
            ViewBag.RegRoles = new MultiSelectList(RoleList(), "Name", "Name", Model.RegRoles);

        }
        public IList<IdentityRole> RoleList()
        {
            var roleStore = new RoleStore<IdentityRole>(_dbIdentityEntities);
            var roleMngr = new RoleManager<IdentityRole>(roleStore);

            var roles = roleMngr.Roles.ToList();
            if (_ActiveSession.IsSuperAdmin || _ActiveSession.IsAdmin)
            {
                roles = roles.ToList();
                return roles;
            }

            if (_ActiveSession.IsOrgAdmin)
            {
                roles = roles.Where(x => x.Name != EnumUserRoles.SuperAdmin.ToString() && x.Name != EnumUserRoles.Admin.ToString()).ToList();
                return roles;
            }

            //roles = roles.Where(x => x.Name == EnumUserRoles.BranchAdmin.ToString() || x.Name == EnumUserRoles.BranchUser.ToString())
            //        .ToList();

            return roles;
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            LoginViewModel login = new LoginViewModel();
            //login.Email = "SuperUser@gmail.com";
            //login.Password = "SuperAdmin123";
            ViewBag.ReturnUrl = returnUrl;
            login.returnUrl = returnUrl;
            Session.Clear();
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            try
            {

                SystemInfoForSession systemSession = new SystemInfoForSession();
                systemSession.RoleId = null;
                systemSession.UserName = null;
                systemSession.UserId = null;
                systemSession.IsAdmin = false;
                //systemSession.RoleName = "";

                if (!ModelState.IsValid)
                {

                    throw new Exception("Invalid Model State. Errors: " + ""
                    //String.Join(',',
                    //ModelState.Values
                    // .Where(x => x.Errors.Count() > 0)
                    // .Select(x => x.Errors)
                    // .ToArray()
                    //)
                    );
                }

                var user = await _userManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    throw new Exception("Invalid User Name.");
                }
                if (user != null)
                {
                    if (user.IsDelete == true)
                    {
                        throw new Exception("User with deleted status cannot make login.");
                    }
                    if (user.IsActive == false)
                    {
                        throw new Exception("User with inactive status cannot make login.");
                    }
                    SignInStatus result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, shouldLockout: false);
                    if (result.ToString() == "Success")
                    {
                        var Role = user.Roles.FirstOrDefault();

                        //if (Role == null)
                        //{
                        //    TempData["ErrMessage"] = "Role Are Not Set This User Please Contact Admin.";
                        //    return RedirectToAction("Login", "Account");
                        //}

                        systemSession.UserId = user.Id;
                        if (Role != null)  //// not using in 1st phase
                        {
                            var roleStore = new RoleStore<IdentityRole>(_dbIdentityEntities);
                            var roleMngr = new RoleManager<IdentityRole>(roleStore);

                            var roles = roleMngr.Roles.ToList();

                            systemSession.RoleIds = user.Roles.ToArray();
                            systemSession.RoleInfoId = user.Roles.Select(s => s.RoleId).ToArray();
                            //
                            systemSession.RoleInfoEnum = roles.Where(x => systemSession.RoleInfoId.Contains(x.Id)).Select(s => s.Name).ToArray();
                            //systemSession.RoleInfoNames = user.Roles.Select(s => s.).ToArray();

                            systemSession.RoleId = Role.RoleId;

                        }
                        IdentityEntities db = new IdentityEntities();

                        var User = _dbMainEntities.usr05users
                        .Include(x => x.bra01branches)
                        .Include(x => x.emp01employee)
                        .SingleOrDefault(x => x.usr05userId == user.Id);

                        if (User != null)
                        {
                            if (User.usr05deleted == true)
                            {
                                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                                throw new Exception("Invalid User Name.");
                            }
                            if (User.emp01employee != null)
                            {
                                systemSession.EmployeeId = User.usr05emp01uin.Value;
                                systemSession.EmployeeName = User.emp01employee.emp01name;
                            }

                            systemSession.BranchId = User.usr05bra01uin;
                            systemSession.BranchName = User.bra01branches.bra01name;
                            systemSession.bra01branches = User.bra01branches;


                        }
                        systemSession.UserName = user.UserName;

                        if (systemSession.DocHandlerID == 0)
                        {
                            //todo: log exception
                        }
                    }
                    else
                    {
                        throw new Exception("User Name Or Password does not match.");
                    }
                }
                Session["SystemInfoForSession"] = systemSession;
                //add menu cache
                //_MenuService.AddMenuCache(systemSession.RoleInfoId);

                ViewHelper.cfg01configurations = _dbMainEntities.cfg01configurations.ToList();

                if (model.returnUrl != null)
                {
                    return RedirectToLocal(model.returnUrl);
                }
                return RedirectToAction("Dashboard", "Home");

            }
            catch (Exception ex)
            {
                FlashBag.setMessage(false, ErrorHelper.GetMsg(ex));
            }
            return View();
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await _signInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes.
            // If a user enters incorrect codes for a specified amount of time then the user account
            // will be locked out for a specified amount of time.
            // You can configure the account lockout settings in IdentityConfig
            var result = await _signInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);

                case SignInStatus.LockedOut:
                    return View("Lockout");

                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowTopUsers]
        [HttpGet]
        public virtual ActionResult Register()
        {
            RegisterViewModel Model = new RegisterViewModel();
            //code for agent drop down
            InitCommon(Model);
            return View();
        }

        [AllowTopUsers]
        [HttpGet]
        public virtual ActionResult RegisterUserEmployee()
        {
            RegisterViewModel Model = new RegisterViewModel();
            InitCommon(Model);
            return View();
        }

        [AllowTopUsers]
        public virtual bool AfterRegister(ApplicationUser RegUser, RegisterViewModel UserData)
        {
            //add user detail usr05.... table also
            //
            return true;
        }

        ////
        // POST: /Account/Register
        [AllowTopUsers]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    user.IsActive = true;
                    await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    //add user roles
                    foreach (var item in model.RegRoles)
                    {
                        IdentityResult x = _userManager.AddToRole(user.Id, item);
                        if (!x.Succeeded)
                        {
                            throw new Exception(string.Join(",", x.Errors.ToArray()));
                        }
                    }

                    AfterRegister(user, model);

                    FlashBag.setMessage(true, "Successfully Created!");
                    try
                    {
                        model.EmployeeName = _EmployeeRepo.GetDetail(model.EmployeeId).emp01name;
                        //_NotificationService.SendNewUserRegister(model);
                    }
                    catch (Exception e)
                    {
                        FlashBag.setMessage(false, "Successfully Created. Email Send Failed Error: " + ErrorHelper.GetMsg(e));
                    }

                    return RedirectToAction("Index");
                }
                AddErrors(result);
            }

            InitCommon(model);
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        public ActionResult UnAuthorizedUser()
        {
            return View();
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByNameAsync(model.UserName);
                    if (user == null)
                    {
                        throw new Exception("User doesn't exists.");
                    }
                    else
                    {
                        try
                        {
                            Random random = new Random();
                            long code = random.Next(100000, 999999);

                            UserCode userCode = new UserCode()
                            {
                                email = user.Email,
                                requested_date = DateTime.Now,
                                valid_till_date = DateTime.Now.AddHours(1),
                                user_id = user.Id,
                                ip = SessionHelper.GetIPAddress(),
                                code = code
                            };
                            _UserCodesRepo.Insert(userCode);
                            _UserCodesRepo.Save();
                            //_NotificationService.SendForgetPasswordCode(user, code);
                        }
                        catch (Exception ex)
                        {
                            FlashBag.setMessage(false, ErrorHelper.GetMsg(ex));
                        }
                        return RedirectToAction("ForgotPasswordConfirmation", new { verification = model.UserName });
                    }
                }
                else
                {
                    throw new Exception("Username is required");
                }
            }
            catch (Exception ex)
            {
                FlashBag.setMessage(false, ErrorHelper.GetMsg(ex));
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPasswordConfirmation(ForgotPasswordViewModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                DateTime current_date = DateTime.Now;
                if (user == null)
                {
                    throw new Exception("User doesn't exists.");
                }
                if (user != null)
                {
                    var check_code = _UserCodesRepo.GetList().OrderByDescending(x => x.id).FirstOrDefault(x => x.code == model.Code && x.user_id == user.Id && (DbFunctions.TruncateTime(x.requested_date) == DbFunctions.TruncateTime(current_date) || DbFunctions.TruncateTime(x.valid_till_date) == DbFunctions.TruncateTime(current_date)));
                    if (check_code != null)
                    {
                        if (check_code.CodeUsed)
                        {
                            throw new Exception("Code already used. Please request new code.");
                        }
                        if (current_date > check_code.valid_till_date)
                        {
                            throw new Exception("Code Expired. Please request new code.");
                        }
                        check_code.CodeUsed = true;
                        _UserCodesRepo.Update(check_code);
                        _UserCodesRepo.Save();
                        return RedirectToAction("ResetPassword", new { verification = model.UserName });
                    }
                    else
                    {
                        throw new Exception("Please Enter Valid Code.");
                    }
                }
            }
            catch (Exception ex)
            {
                FlashBag.setMessage(false, ErrorHelper.GetMsg(ex));
            }
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation(string verification)
        {
            ForgotPasswordViewModel model = new ForgotPasswordViewModel();
            model.UserName = verification;
            return View(model);
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string verification)
        {
            ResetPasswordViewModel model = new ResetPasswordViewModel();
            model.UserName = verification;
            return View(model);
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            try
            {
                if (model.Password != model.ConfirmPassword)
                {
                    throw new Exception("Confirmed Password not matched.");
                }

                DateTime current_date = DateTime.Now;
                    var user = await _userManager.FindByNameAsync(model.UserName);
                var check_code = _UserCodesRepo.GetList().OrderByDescending(x => x.id).FirstOrDefault(x => x.user_id == user.Id && (DbFunctions.TruncateTime(x.requested_date) == DbFunctions.TruncateTime(current_date) || DbFunctions.TruncateTime(x.valid_till_date) == DbFunctions.TruncateTime(current_date)));
                if (check_code != null)
                {
                    if (current_date > check_code.valid_till_date)
                    {
                        throw new Exception("Code Expired. Please Try Again.");
                    }
                    var result = await _userManager.ChangePasswordAdmin(user.Id, model.Password);
                    if (result.Succeeded)
                    {
                        try
                        {
                            var usr05data = _UserRepo.GetDetail(user.Id);
                            //_NotificationService.SendPasswordReset(user, usr05data);
                            FlashBag.setMessage(true, "Password Changed Successfully.");
                        }
                        catch (Exception ex)
                        {
                            FlashBag.setMessage(true, "Password Changed Successfully. Error Sending Mail: " + ErrorHelper.GetMsg(ex));
                        }
                        return RedirectToAction("Login", "Account");
                    }
                    AddErrors(result);
                }
                else
                {
                    throw new Exception("Please Enter Valid Code.");
                }
            }
            catch (Exception ex)
            {
                FlashBag.setMessage(false, ErrorHelper.GetMsg(ex));
            }
            return View(model);
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await _signInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await _userManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await _signInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await _signInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);

                case SignInStatus.LockedOut:
                    return View("Lockout");

                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });

                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        public ActionResult LogOff()
        {
            var systemSession = (SystemInfoForSession)Session["SystemSession"];

            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        #region Helpers

        // Used for XSRF protection when adding external logins
        protected const string XsrfKey = "XsrfId";

        protected IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        #endregion Helpers

        [AllowAnonymous]
        public ActionResult ChangePassword()
        {
            if (_ActiveSession.UserId == null)
            {

                return RedirectToAction("Login");
            }
            @ViewBag.username = _ActiveSession.UserName;
            return View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {

            try
            {
                if (model.username == null)
                {
                    throw new Exception("User Name is Null");
                }
                if (model.OldPassword == null || model.OldPassword == "")
                {
                    throw new Exception("old Password is Blank");
                }
                if (model.Password == null || model.Password == "")
                {
                    throw new Exception("new Password is Blank");
                }
                var user = await _userManager.FindByNameAsync(model.username);
                if (user == null)
                {
                    // Don't reveal that the user does not exist
                    return RedirectToAction("ChangePassword", "Account");
                }
                var result = _userManager.ChangePassword(user.Id, model.OldPassword, model.Password);

                if (result.Succeeded)
                {
                    FlashBag.setMessage(true, "Successfully Change Password");
                    return RedirectToAction("ChangePassword", "Account");
                }
                else
                {
                    throw new Exception("Sorry Password have not been change");
                }

                //AddErrors(result);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException != null ? ex.InnerException.InnerException != null ? ex.InnerException.InnerException.Message : ex.InnerException.Message : ex.Message);
                FlashBag.setMessage(false, ex.InnerException != null ? ex.InnerException.InnerException != null ? ex.InnerException.InnerException.Message : ex.InnerException.Message : ex.Message);
            }
            @ViewBag.username = _ActiveSession.UserName;
            return View();
        }
    }
}