//using DMS.DAL;
//using DMS.DAL.DatabaseContext;
//using DMS.DAL.EntityModels;
//using DMS.DAL.Helpers;
//using DMS.DAL.Interfaces;
//using DMS.DAL.Repositories.GeneralRepo.Interfaces;
//using DMS.DAL.StaticHelper;
//using DMS.Helpers;
//using DMS.Models;
//using DMS.Services.General.Interface;
//using DMS.ViewModels;
//using DMS.ViewModels.VMEmail;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace DMS.Services
//{
//    public class NotificationService
//    {
//        public readonly SystemInfoForSession _ActiveSession;
//        private readonly IEmployeeRepo _EmployeeRepo;
//        private static bool _HasEmailNotification;
//        private static EmailServerSetting _EmailConfig;
//        private readonly IConfigValuesByEnumRepo _ConfigRepo;
//        private EmailHelperService _EmailHelperService;
//        private bool EmailNotification;

//        //public system MyProperty { get; set; }
//        public NotificationService(
//            IEmployeeRepo EmployeeRepo,
//            IConfigValuesByEnumRepo ConfigRepo,
//            EmailHelperService EmailHelperService
//        )
//        {
//            _ActiveSession = SessionHelper.GetSession();
//            _EmployeeRepo = EmployeeRepo;
//            _EmailHelperService = EmailHelperService;
//            _ConfigRepo = ConfigRepo;

//            EmailNotification = _ConfigRepo.GetBool(enumConfigSettingsKeys.EmailNotification);

//            string password = BasicEncryption.DecryptString(_ConfigRepo.GetString(enumConfigSettingsKeys.SMTPPassword), Properties.Settings.Default.EncryptPass);
//            _EmailConfig = new EmailServerSetting()
//            {
//                EnableSSL = _ConfigRepo.GetBool(enumConfigSettingsKeys.SMTPSSL),
//                SMTP_From = _ConfigRepo.GetString(enumConfigSettingsKeys.SMTPFrom),
//                SMTP_Pass = password,
//                SMTP_Port = _ConfigRepo.GetInt(enumConfigSettingsKeys.SMTPPort),
//                SMTP_Server = _ConfigRepo.GetString(enumConfigSettingsKeys.SMTPServer),
//                SMTP_User = _ConfigRepo.GetString(enumConfigSettingsKeys.SMTPUser)
//            };

//            if (_EmailConfig.SMTP_Server == "") { throw new Exception("Email Server configuration mismtched."); }
//        }

//        private void SendMail(EnumTemplateList EnumTemplateList, EmailTemplateValueVM Data)
//        {
//            if (EmailNotification)
//            {
//                string MailBody = _TemplateHelperServices.GetTemplateValue(EnumTemplateList, Data);
//                string MailSubject = _TemplateHelperServices.GetTemplateSubject(EnumTemplateList, Data);
//                _EmailHelperService.SendMail(Data.EmailList, MailSubject, MailBody);
//            }
//        }

//        public void SendForgetPasswordCode(ApplicationUser Data, long code)
//        {
//            EmailTemplateValueVM X = new EmailTemplateValueVM()
//            {
//                UserName = Data.UserName,
//                UserEmail = Data.Email,
//                PassCode = code.ToString(),
//                EmailList = new string[] { Data.Email }
//            };

//            SendMail(EnumTemplateList.ForgetPasswordTemplate, X);

//        }

//        public void SendPasswordReset(ApplicationUser Data, usr05users usr05Data)
//        {
//            EmailTemplateValueVM X = new EmailTemplateValueVM()
//            {
//                UserName = Data.UserName,
//                UserEmail = Data.Email,
//                EmailList = new string[] { Data.Email },
//                EmployeeName = usr05Data.emp01employee?.emp01name
//            };

//            SendMail(EnumTemplateList.PasswordReset, X);

//        }

//        public void SendNewUserRegister(RegisterViewModel Data)
//        {
//            EmailTemplateValueVM X = new EmailTemplateValueVM()
//            {
//                EmployeeName = Data.EmployeeName,
//                UserName = Data.UserName,
//                UserEmail = Data.Email,
//                Password = Data.Password,
//                LogedInUserName = _ActiveSession.EmployeeName,
//                EmailList = new string[] { Data.Email }
//            };

//            SendMail(EnumTemplateList.NewUserRegister, X);

//        }

//        public void SendSinlgeDispatchedMail(EnumTemplateList EnumTemplateList, req06debit_card_print_details Data)
//        {
//            try
//            {
//                var branch_manager = Data.req03debit_card_requests.req01service_requests.bra01branches.bra02branch_manager;
//                string emails = branch_manager != null && branch_manager.bra02email != null && branch_manager.bra02email != "" ? branch_manager.bra02email : branch_manager?.emp01employee?.emp01email;
//                if (emails == null)
//                {
//                    throw new Exception("Branch Manager Mail not found. Please setup Branch Manager");
//                }

//                EmailTemplateValueVM X = new EmailTemplateValueVM()
//                {
//                    AccountNumber = Data.req03debit_card_requests.req03account_no,
//                    BatchNo = Data.req05debit_card_print_batches.req05batch_no.ToString(),
//                    CardNumber = Data.req03debit_card_requests.card01overall_cards?.card01code,
//                    CustomerName = Data.req03debit_card_requests.req03print_name,
//                    RefNo = Data.req03debit_card_requests.req01service_requests.req01ref_code,
//                    EmailList = emails.Split(',')
//                };

//                SendMail(EnumTemplateList, X);

//            }
//            catch (Exception e)
//            {
//                FlashBag.setMessage(false, "Successfully Dispatched. Email Send Failed Error: " + ErrorHelper.GetMsg(e));
//            }
//        }

//        public void SendAllDispatchedMail(EnumTemplateList EnumTemplateList, List<req06debit_card_print_details> Data)
//        {
//            try
//            {
//                var branch_manager = Data.FirstOrDefault().req03debit_card_requests.req01service_requests.bra01branches.bra02branch_manager;
//                string emails = branch_manager != null && branch_manager.bra02email != null && branch_manager.bra02email != "" ? branch_manager.bra02email : branch_manager?.emp01employee?.emp01email;
//                if (emails == null)
//                {
//                    throw new Exception("Branch Manager Mail not found. Please setup Branch Manager");
//                }

//                EmailTemplateValueVM X = new EmailTemplateValueVM()
//                {
//                    BatchTotalCard = Data.Count().ToString(),
//                    BatchNo = Data.FirstOrDefault()?.req05debit_card_print_batches.req05batch_no.ToString(),
//                    EmailList = emails.Split(',')
//                };

//                SendMail(EnumTemplateList, X);

//            }
//            catch (Exception e)
//            {
//                ErrorLogger.LogException(e);
//                FlashBag.setMessage(false, "Successfully Dispatched. Email Send Failed Error: " + ErrorHelper.GetMsg(e));
//            }
//        }



//    }
//}