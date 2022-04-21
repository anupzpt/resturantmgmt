using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMS.ViewModels
{
    public class SearchFilterVM
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? BranchID { get; set; }
        public int? CustomerID { get; set; }
        public string CardNo { get; set; }
        public string AccountNo { get; set; }
        public string RequestType { get; set; }
        public int? CardStatus { get; set; }
        public byte? CardType { get; set; }
    }

    public class BatchSearchFilterVM
    {
        public int? FromBatch { get; set; }
        public int? ToBatch { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? BranchID { get; set; }
    }
}