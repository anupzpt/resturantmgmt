using DMS.DAL;
using DMS.DAL.DatabaseContext;
using DMS.DAL.Helpers;
using DMS.DAL.Repositories.GeneralRepo.Interfaces;
using DMS.DAL.StaticHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DMS.Services
{
    public class EmailHelperService
    {
        private MainEntities db;
        private SystemInfoForSession _ActiveSession;
        private readonly IConfigValuesByEnumRepo _ConfigRepo;
        EmailHelper emailHelper;

        public EmailHelperService(MainEntities dbMain, IConfigValuesByEnumRepo ConfigRepo)
        {
            db = dbMain;
            _ActiveSession = SessionHelper.GetSession();
            _ConfigRepo = ConfigRepo;
            string password = BasicEncryption.DecryptString(_ConfigRepo.GetString(enumConfigSettingsKeys.SMTPPassword), Properties.Settings.Default.EncryptPass);

            EmailServerSetting config = new EmailServerSetting
            {
                EnableSSL = _ConfigRepo.GetBool(enumConfigSettingsKeys.SMTPSSL),
                SMTP_From = _ConfigRepo.GetString(enumConfigSettingsKeys.SMTPFrom),
                SMTP_Pass = password,
                SMTP_Port = _ConfigRepo.GetInt(enumConfigSettingsKeys.SMTPPort),
                SMTP_Server = _ConfigRepo.GetString(enumConfigSettingsKeys.SMTPServer),
                SMTP_User = _ConfigRepo.GetString(enumConfigSettingsKeys.SMTPUser)
            };
            emailHelper = new EmailHelper(config);
        }

        public void SendMail(string[] emails, string Subject, string MailBody)
        {
            emailHelper.DispatchMail(emails, Subject, MailBody);
        }

        public void SendMail(string emails, string Subject, string MailBody)
        {
            emailHelper.DispatchMail(emails, Subject, MailBody);
        }

        public static void SendMail(long lon02uin, ControllerContext controllerContext, HttpServerUtilityBase Server)
        {
            //lon02reference lon02reference = db.lon02reference.Find(lon02uin);
            //ViewAsPdf pdf = new ViewAsPdf("Partial/CICLCommonDetails/CICLPrint", lon02reference)
            //{
            //    FileName = "CICLPDF_" + lon02uin + "_" + _ActiveSession.SysDateEng.ToString("yyyyMMddhhmmss") + ".pdf",
            //};
            //byte[] pdfData = pdf.BuildFile(controllerContext);
            //string fullPath = Server.MapPath("~\\" + _ConfigRepo.GetString(enumConfigSettingsKeys.GeneralSettings_CICLPdfFilePath));

            //if (!Directory.Exists(fullPath))
            //{
            //    Directory.CreateDirectory(fullPath);
            //}
            //fullPath = Path.Combine(fullPath, pdf.FileName);
            //using (var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            //{
            //    fileStream.Write(pdfData, 0, pdfData.Length);
            //}

            //string[] emails = _ConfigRepo.GetString(enumConfigSettingsKeys.CICLEmailList).Split(',').ToArray();
            //string password = BasicEncryption.DecryptString(_ConfigRepo.GetString(enumConfigSettingsKeys.SMTPPassword), Properties.Settings.Default.EncryptPass);
            //emailHelper.DispatchAttachementMail(emails, password, lon02reference.cus01customer_comming_info.cus01name_eng + " CICL Mail", "", fullPath);

        }
    }
}