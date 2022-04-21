using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMS.ViewModels.Report
{
    public class RepinRequestVM
    {
        public long req01uin { get; set; }
        public long? card01uin { get; set; }
        public string RefNo { get; set; }
        public string ClientID { get; set; }
        public string CustomerName { get; set; }
        public string AccountNumber { get; set; }
        public string BatchNumber { get; set; }
        public string CardNumber { get; set; }
        public string CollectionBranch { get; set; }
        public string PinReceivedDate { get; set; }
        public string PinDispatchDate { get; set; }
        public string Application { get; set; }
        public string RequestedBy { get; set; }
        public string ApproveBy { get; set; }
        public string ApprovedDate { get; set; }
        public bool ChargeStatus { get; set; }
    }
}