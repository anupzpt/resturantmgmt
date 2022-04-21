using DMS.DAL.StaticHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DMS.DAL.Helpers
{
    public class AuthHelper
    {
        public string SessKey => HttpContext.Current.Session.SessionID;
        public SystemInfoForSession SessionData
        {
            get
            {
                return (SystemInfoForSession)HttpContext.Current.Session["SystemInfoForSession"];
            }
            set
            {
                HttpContext.Current.Session["SystemInfoForSession"] = value;
            }
        }
    }
}
