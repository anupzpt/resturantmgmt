using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DMS.Helpers
{
    public static class RestSharpHelper
    {
        public static bool Log<DataType>(IRestResponse<DataType> Response)
        {
            string myLocation = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/private/"), "CCMS/");
            if (!System.IO.Directory.Exists(myLocation)) { Directory.CreateDirectory(myLocation); }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter($"{myLocation}/log_{DateTime.Now.ToString("yyyy_MM_dd")}.txt", true))
            {
                file.WriteLine(JsonConvert.SerializeObject(Response));
            }
            return true;
        }

        public static bool Log(string Response)
        {
            string myLocation = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/private/"), "Logs/");
            if (!System.IO.Directory.Exists(myLocation)) { Directory.CreateDirectory(myLocation); }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter($"{myLocation}/log_{DateTime.Now.ToString("yyyy_MM_dd")}.txt", true))
            {
                file.WriteLine(JsonConvert.SerializeObject(Response));
            }
            return true;
        }
    }
}