using DMS.DAL.DatabaseContext;
using DMS.ViewModels;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace DMS.Helpers
{
    public class RestApiHelper
    {
        private string API_KEY { get; set; }
        public string base_URL { get; private set; }
        public string Prefix { get; private set; }

        protected readonly IdentityEntities _db;
        public RestApiHelper(IdentityEntities dbCore)
        {
            _db = dbCore;
            Init();
        }

        public void Init()
        {
            API_KEY = "";
            base_URL = "http://localhost:49384/api/";
            Prefix = "";
        }

        public static string ResponseDetail(string code, string response_msg)
        {
            IDictionary<string, string> _Codes = new Dictionary<string, string>();
            _Codes.Add("000", "Account successfully validated.");
            _Codes.Add("999", "Some differences in beneficiary account name observed. Transaction once sent is irreversible, please reconfirm the beneficiary account number.");
            _Codes.Add("523", "Beneficiary account name mismatch.");
            _Codes.Add("502", "Beneficiary account does not exist.");
            _Codes.Add("700", "Empty values obtained.");
            _Codes.Add("701", "Invalid bank.");
            _Codes.Add("1001", "Bank network not reachable.");

            return _Codes.ContainsKey(code) ? _Codes[code] : "Error Unknown Response code: " + code + " Content: " + response_msg;
        }

        public static string ResponseDetailDebitCode(string code, string response_msg)
        {
            IDictionary<string, string> _Codes = new Dictionary<string, string>();
            _Codes.Add("000", "Debit Success");
            _Codes.Add("999", "Debit ISO Time Out.");
            _Codes.Add("1000", "Debit rejected");
            _Codes.Add("1001", "Bank network not reachable.");
            _Codes.Add("2001", "ANTS Limit Exceeds.");

            return _Codes.ContainsKey(code) ? _Codes[code] : "Error Unknown Response code: " + code + " Content: " + response_msg;
        }

        public static string ResponseDetailCreditCode(string code, string response_msg)
        {
            IDictionary<string, string> _Codes = new Dictionary<string, string>();
            _Codes.Add("000", "Credit  Success");
            _Codes.Add("999", "Credit  ISO Time Out.");
            _Codes.Add("1000", "Credit  rejected");
            _Codes.Add("1001", "Bank network not reachable.");

            return _Codes.ContainsKey(code) ? _Codes[code] : "Error Unknown Response code: " + code + " Content: " + response_msg;
        }

        public static string SanitizeAccountNo(string RawAccNo)
        {
            return Regex.Replace(RawAccNo, @"[^0-9a-zA-Z]+", "");
        }

        
    }
}