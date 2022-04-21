using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMS.ViewModels
{
    public class SortingVM
    {
        public long Id { get; set; }
        public string BatchNo { get; set; }
        public bool CardSorted { get; set; }
        public string SortedType { get; set; }
        public long Count { get; set; }
        public string Date { get; set; }
        public string SortingDetailsUrl { get; set; }
        public string SortedBy { get; set; }
    }

    public class SortingDetailsVM
    {
        public long sort02uin { get; set; }
        public int req01uin { get; set; }
        public string Branch { get; set; }
    }
}