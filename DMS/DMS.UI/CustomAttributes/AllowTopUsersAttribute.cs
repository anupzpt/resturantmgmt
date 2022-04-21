using DMS.DAL.StaticHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DMS.CustomAttributes
{
    public class AllowTopUsersAttribute : ActionFilterAttribute
    {
        public string URL { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            SystemInfoForSession user = SessionHelper.GetSession();

            var action = filterContext.ActionDescriptor;
            if (action.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                if (user == null)
                {
                    filterContext.Result = new RedirectResult(string.Format(URL == null ? "/" : URL, filterContext.HttpContext.Request.Url.AbsolutePath));
                }
                return;
            }
            if (!user.IsSuperAdmin && !user.IsOrgAdmin)
            {
                FlashBag.setMessage(true, "Authorization Failed!");
                filterContext.Result = new RedirectResult(string.Format(URL == null ? "/" : URL, filterContext.HttpContext.Request.Url.AbsolutePath));
            }
        }

    }

}