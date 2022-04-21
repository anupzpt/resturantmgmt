using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMS
{
    public static class FlashBag
    {
        private static string _msg;
        private static bool _cls;
        private static string getSession(string name)
        {
            return (HttpContext.Current.Session[name] != null) ? HttpContext.Current.Session[name].ToString() : "";
        }
        public static void setMessage(bool isSuccess, string msg)
        {
            HttpContext.Current.Session["flash_msg"] = msg;
            HttpContext.Current.Session["flash_class"] = isSuccess;
        }
        public static string showMessage()
        {
            if (hasMessage())
            {
                return string.Format("<div class='alert alert-{1}'>{0}</div>",
                    _msg, (_cls ? "success" : "danger")
                    );
            }
            return "";
        }
        public static bool hasMessage()
        {
            string msg = getSession("flash_msg");
            bool cs = false;
            bool.TryParse(getSession("flash_class"), out cs);
            _msg = msg;
            _cls = cs;
            HttpContext.Current.Session.Remove("flash_msg");
            HttpContext.Current.Session.Remove("flash_class");
            return (!(_msg == "" && _cls == false));
        }
        public static string getJSON()
        {
            IDictionary<string, string> res = new Dictionary<string, string>();
            res.Add("Stat", getSession("flash_msg"));
            res.Add("Msg", getSession("flash_class"));

            return JsonConvert.SerializeObject(res);
        }
        public static string getClass()
        {
            return (_cls ? "success" : "danger");
        }
        public static string getMessage()
        {
            return _msg;
        }
    }
}
