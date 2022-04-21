using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMS.ViewModels.Report
{
    public class CardDetailReportVM
    {
        public long req01uin { get; set; }
        public long? card01uin { get; set; }
        public string RefNo { get; set; }
        public string ClientID { get; set; }
        public string CustomerName { get; set; }
        public string AccountNumber { get; set; }
        public string CardNumber { get; set; }
        public string Branch { get; set; }
        public string CardType { get; set; }
        public string CardIssueDate { get; set; }
        public string ExpiryDate { get; set; }
        public string LastRenewDate { get; set; }
        public string CardStatus { get; set; }
        public string CardRequestedBy { get; set; }
        public string CardApprovedBy { get; set; }
        public string CardActivatedDate { get; set; }
        public string CardActivatedBy { get; set; }
    }
}