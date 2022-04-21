using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;

namespace DMS.DAL.StaticHelper
{
    public static class SessionHelper
    {
        public static void SetSession(SystemInfoForSession Data)
        {
            HttpContext.Current.Session["SystemInfoForSession"] = Data;
        }
        public static SystemInfoForSession GetSession()
        {
            return (SystemInfoForSession)HttpContext.Current.Session["SystemInfoForSession"];
        }

        public static string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
        public static IpInfo GetUserIpInfo(string ip)
        {
            IpInfo ipInfo = new IpInfo() { Ip = ip, City = "Chabahil", Country = "Nepal" };
            if (!Properties.Settings.Default.GetExternalIPInfo) { return ipInfo; }
            try
            {
                string info = new WebClient().DownloadString("http://ipinfo.io/" + ip);
                ipInfo = JsonConvert.DeserializeObject<IpInfo>(info);
                RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRI1.EnglishName;
            }
            catch (Exception)
            {
                ipInfo.Country = null;
            }
            ipInfo.Ip = ip;
            if (ipInfo.City == null) { ipInfo.City = "Chabahil"; }
            return ipInfo;
        }
        public static IpInfo GetUserIpInfo()
        {
            return GetUserIpInfo(GetIPAddress());
        }
    }
    public class IpInfo
    {

        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("hostname")]
        public string Hostname { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("loc")]
        public string Loc { get; set; }

        [JsonProperty("org")]
        public string Org { get; set; }

        [JsonProperty("postal")]
        public string Postal { get; set; }

    }
}