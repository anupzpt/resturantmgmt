using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMS.ViewModels.Report
{
    public class Block_UnblockReportVM
    {
        public long req01uin { get; set; }
        public long? card01uin { get; set; }
        public string RefNo { get; set; }
        public string ClientID { get; set; }
        public string CustomerName { get; set; }
        public string AccountNumber { get; set; }
        public string CardNumber { get; set; }
        public string RequestBranch { get; set; }
        public string BlockedUnblockedBy { get; set; }
        public DateTime? BlockedUnblockedDate { get; set; }
        public string BlockedUnblockApplication { get; set; }
        public string RequestedBy { get; set; }
        public string Ext { get; set; }
        public bool IsImage { get; set; }
        public bool ChargeStatus { get; set; }
    }
}