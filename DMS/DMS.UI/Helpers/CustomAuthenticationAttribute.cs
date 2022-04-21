using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace DMS
{
    public class CustomAuthenticationAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext context)
        {
            var action = context.ActionDescriptor;
            if (action.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                return;
            }

            //do not check login for AllowAnonymousAttribute in controller
            var controller = action.ControllerDescriptor;
            if (controller.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                return;
            }


            var httpContext = new HttpContextWrapper(HttpContext.Current);

         

            RouteData urlRouteData = RouteTable.Routes.GetRouteData(httpContext);
            string controllerName = urlRouteData.Values["controller"].ToString();
            string actionName = urlRouteData.Values["action"].ToString();
            string[] AllowedController = new[] { "Account", "OnlineForm" };


            if (!(((controllerName == "Account") && (actionName == "Login" || actionName == "LogOff")) || (controllerName.ToUpper() == "OnlineForm".ToUpper())))
            {
                if (HttpContext.Current.Session["SystemInfoForSession"] == null)
                {
                    context.Result = new HttpUnauthorizedResult();
                }
            }
          

        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext context)
        {
            if (context.Result == null || context.Result is HttpUnauthorizedResult)
            {
                context.Result = new RedirectToRouteResult("Default",
                new System.Web.Routing.RouteValueDictionary{
                        {"controller", "Account"},
                        {"action", "Login"},
                        {"returnUrl", context.HttpContext.Request.RawUrl}
                });
            }
        }

    }


}