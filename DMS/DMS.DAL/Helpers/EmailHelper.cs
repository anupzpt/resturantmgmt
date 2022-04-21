using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Helpers
{
    public class EmailServerSetting
    {
        public string SMTP_Server;
        public string SMTP_User;
        public string SMTP_Pass;
        public int SMTP_Port;
        public string SMTP_From;
        public bool EnableSSL;
        public EmailServerSetting()
        {
            EnableSSL = false;
        }
    }
    public class EmailHelper
    {
        public readonly EmailServerSetting _Config;
        public EmailHelper(EmailServerSetting Config)
        {
            _Config = Config;
        }
        public bool DispatchMail(string[] ToEmails, string Subject, string MailBody)
        {
            if (_Config.SMTP_Server == "") { throw new Exception("Invalid Email Server."); }
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(_Config.SMTP_Server);
            mail.From = new MailAddress(_Config.SMTP_From);
            foreach (var item in ToEmails)
            {
                mail.To.Add(new MailAddress(item));
            }
            mail.Subject = Subject;
            mail.Body = MailBody;
            mail.IsBodyHtml = true;
            SmtpServer.Port = _Config.SMTP_Port;
            SmtpServer.Credentials = new System.Net.NetworkCredential(_Config.SMTP_User, _Config.SMTP_Pass);
            SmtpServer.EnableSsl = _Config.EnableSSL;
            SmtpServer.Send(mail);
            return true;
        }

        public bool DispatchAttachementMail(string[] ToEmails, string Subject, string MailBody, string filePath)
        {
            if (_Config.SMTP_Server == "") { throw new Exception("Invalid Email Server."); }
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(_Config.SMTP_Server);
            mail.From = new MailAddress(_Config.SMTP_From);
            foreach (var item in ToEmails)
            {
                mail.To.Add(new MailAddress(item));
            }
            mail.Subject = Subject;
            mail.Attachments.Add(new Attachment(filePath));
            mail.IsBodyHtml = true;
            SmtpServer.Port = _Config.SMTP_Port;
            SmtpServer.Credentials = new System.Net.NetworkCredential(_Config.SMTP_User, _Config.SMTP_Pass);
            SmtpServer.EnableSsl = _Config.EnableSSL;
            SmtpServer.Send(mail);
            return true;
        }

        public bool DispatchMail(string ToEmail, string Subject, string MailBody)
        {
            return DispatchMail(new string[] { ToEmail }, Subject, MailBody);
        }
    }
}
