//using Microsoft.AspNet.Identity;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Mvc.Filters;
//using System.Web.Routing;

//namespace IMS.Services.Helper
//{
//    public class CustomAuthenticationAttribute : ActionFilterAttribute, IAuthenticationFilter
//    {
//        public void OnAuthentication(AuthenticationContext context)
//        {
//            var httpContext = new HttpContextWrapper(HttpContext.Current);
//            //get ConrollerActionName From URL
//            RouteData urlRouteData = RouteTable.Routes.GetRouteData(httpContext);
//            string controllerName = urlRouteData.Values["controller"].ToString();
//            string actionName = urlRouteData.Values["action"].ToString();

//            if (((controllerName == "Home") && (actionName == "Index")) || ((controllerName == "Home") && (actionName == "Error")) || ((controllerName == "Account") && (actionName == "Login")) || ((controllerName == "Account") && (actionName == "LogOff") || ((controllerName == "Account") && (actionName == "UnAuthorizedUser")))) { }
//            else
//            {
//                var sessionSystem = (SystemInfoForSession)HttpContext.Current.Session["SystemSession"];
//                if (sessionSystem == null)
//                {
//                    context.Result = new HttpUnauthorizedResult();
//                }
//                else
//                {
//                    if (HttpContext.Current.User.Identity.IsAuthenticated)
//                    {
//                        var userName = HttpContext.Current.User.Identity.GetUserName();
//                        if (!sessionSystem.IsAdmin && !sessionSystem.IsSuperAdmin)
//                        {
//                            var ControllerSession = (List<ActionRole>)HttpContext.Current.Session["actionWithRoleList" + userName];
//                            if (ControllerSession == null)
//                            {
//                                // For Set The All ControllerActionWithRoleList Associated with this user
//                                CheckAuthorized check = new CheckAuthorized();
//                                check.checkAuthorized(userName);
//                                ControllerSession = (List<ActionRole>)HttpContext.Current.Session["actionWithRoleList" + userName];
//                            }
//                            //if (!SystemInfo.IsBeginOFDay)
//                            //{
//                            //    ControllerSession = ControllerSession.Where(x => x.ControllerAction.ActiveAllTime == true).ToList();
//                            //}
//                            var CheckData = ControllerSession.Any(x => x.ControllerAction.ControllerName == controllerName && x.ControllerAction.ActionName == actionName);
//                            if (!CheckData)
//                            {
//                                context.Result = new HttpUnauthorizedResult();
//                            }
//                        }
//                    }
//                    else
//                    {
//                        context.Result = new HttpUnauthorizedResult();
//                    }
//                }
//            }
//        }

//        public void OnAuthenticationChallenge(AuthenticationChallengeContext context)
//        {
//            var sessionSystem = (SystemInfoForSession)HttpContext.Current.Session["SystemSession"];

//            if (context.Result == null || context.Result is HttpUnauthorizedResult)
//            {
//                if (sessionSystem != null)
//                {
//                    context.Result = new RedirectToRouteResult("Default",
//                    new System.Web.Routing.RouteValueDictionary
//                    {
//                {"controller", "Account"},
//                {"action", "UnAuthorizedUser"},
//                {"returnUrl", context.HttpContext.Request.RawUrl}
//                    });
//                }
//                else
//                {
//                    context.Result = new RedirectToRouteResult("Default",
//                    new System.Web.Routing.RouteValueDictionary{
//                {"controller", "Account"},
//                {"action", "Login"},
//                {"returnUrl", context.HttpContext.Request.RawUrl}
//                    });
//                }
//            }
//        }
//    }
//}