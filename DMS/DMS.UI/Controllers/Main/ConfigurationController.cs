using DMS.DAL;
using DMS.DAL.DatabaseContext;
using DMS.DAL.Helpers;
using DMS.DAL.Repositories.GeneralRepo.Interfaces;
using DMS.DAL.StaticHelper;
using DMS.ViewModels.VMEmail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DMS.Controllers
{
    public class ConfigurationController : Controller
    {
        private MainEntities db;
        private IdentityEntities _dbIdentityEntities;
        private SystemInfoForSession _ActiveSession;
        private readonly IConfigValuesByEnumRepo _ConfigRepo;

        public ConfigurationController(MainEntities _db, IConfigValuesByEnumRepo ConfigRepo)
        {
            db = _db;
            _dbIdentityEntities = new IdentityEntities();
            _ActiveSession = SessionHelper.GetSession();
            _ConfigRepo = ConfigRepo;
        }

        // GET: Configuration
        public ActionResult Index()
        {
            if (_ActiveSession.IsOrgAdmin)
            {
                IList<cfg01configurations> cfg01configurations = db.cfg01configurations.ToList();
                return View(cfg01configurations);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Edit()
        {
            if (_ActiveSession.IsOrgAdmin)
            {
                VMEditSettings data = new VMEditSettings();
                data.SMTPServer = _ConfigRepo.GetString(enumConfigSettingsKeys.SMTPServer);
                data.SMTPUser = _ConfigRepo.GetString(enumConfigSettingsKeys.SMTPUser);
                data.SMTPPassword = _ConfigRepo.GetString(enumConfigSettingsKeys.SMTPPassword);
                data.SMTPPort = _ConfigRepo.GetInt(enumConfigSettingsKeys.SMTPPort);
                data.SMTPSSL = _ConfigRepo.GetBool(enumConfigSettingsKeys.SMTPSSL);
                data.SMTPFrom = _ConfigRepo.GetString(enumConfigSettingsKeys.SMTPFrom);
                data.EmailNotification = _ConfigRepo.GetBool(enumConfigSettingsKeys.EmailNotification);
                return View(data);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Edit(VMEditSettings Data)
        {
            try
            {
                if (_ActiveSession.IsOrgAdmin)
                {
                    _ConfigRepo.Update(enumConfigSettingsKeys.SMTPServer, Data.SMTPServer);
                    _ConfigRepo.Update(enumConfigSettingsKeys.SMTPUser, Data.SMTPUser);

                    string old_pass = _ConfigRepo.GetString(enumConfigSettingsKeys.SMTPPassword);
                    if (old_pass != Data.SMTPPassword)
                    {
                        var encrypted_pass = BasicEncryption.EncryptString(Data.SMTPPassword, Properties.Settings.Default.EncryptPass);
                        _ConfigRepo.Update(enumConfigSettingsKeys.SMTPPassword, encrypted_pass);
                    }
                    _ConfigRepo.Update(enumConfigSettingsKeys.SMTPPort, Data.SMTPPort);
                    _ConfigRepo.Update(enumConfigSettingsKeys.SMTPSSL, Data.SMTPSSL);
                    _ConfigRepo.Update(enumConfigSettingsKeys.SMTPFrom, Data.SMTPFrom);
                    _ConfigRepo.Update(enumConfigSettingsKeys.EmailNotification, Data.EmailNotification);
                    ViewHelper.cfg01configurations = db.cfg01configurations.ToList();

                    FlashBag.setMessage(true, "Configuration Updated Successfully");
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ErrorHelper.GetMsg(ex));
                FlashBag.setMessage(false, ErrorHelper.GetMsg(ex));
            }
            return RedirectToAction("Edit");
        }

    }
}