using System;

namespace DMS.UI.ViewModels.ErrorLog
{
    public class GlobalErrorLogVM
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Error { get; set; }
        public string Attribute { get; set; }
        public DateTime EnglishDate { get; set; }
        public string NepaliDate { get; set; }
        public string ClientIpAddress { get; set; }
        public string ClientName { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public bool Resolved { get; set; }
    }
}