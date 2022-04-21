using DMS.DAL.StaticHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace DMS.Helpers
{
    public class BlockSuperAdminAttribute : ActionFilterAttribute
    {
        public string URL { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            SystemInfoForSession user = SessionHelper.GetSession();

            if (user.IsSuperAdmin)
            {
                filterContext.Result = new RedirectResult(string.Format(URL == null ? "/Home/AccessDeniedPage" : URL, filterContext.HttpContext.Request.Url.AbsolutePath));
            }
        }

    }

}