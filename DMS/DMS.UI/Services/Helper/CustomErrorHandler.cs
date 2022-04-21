using DMS.DAL.DatabaseContext;
using DMS.DAL.StaticHelper;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;
using DMS.DAL.Helpers;
using DMS.DAL.EntityModels;

namespace IMS.Services.Helper
{
    public class CustomErrorHandler : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            try
            {
                //var systemSession = (SystemInfoForSession)HttpContext.Current.Session["SystemSession"];
                //if (systemSession == null)
                //{
                //    filterContext.Result = new RedirectToRouteResult("Default", new System.Web.Routing.RouteValueDictionary{{"controller", "Account"},
                //            {"action", "Login"},
                //            {"returnUrl", filterContext.HttpContext.Request.RawUrl}});
                //}
                //else
                //{
                Exception e = filterContext.Exception;
                var controllerName = filterContext.RouteData.Values["controller"];
                var actionName = filterContext.RouteData.Values["action"];
                var request = filterContext.HttpContext.Request.RequestType;
                filterContext.ExceptionHandled = true;
                using (IdentityEntities storeDb = new IdentityEntities())
                {
                    var errorLog = new GlobalErrorLog()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Controller = controllerName.ToString(),
                        Action = actionName.ToString(),
                        Attribute = request,
                        Title = e.Message,
                        Error = JsonConvert.SerializeObject(e),
                        Resolved = false,
                        ClientIpAddress = filterContext.HttpContext.Request.UserHostAddress,
                        ClientName = filterContext.HttpContext.Request.UserHostName,
                        EnglishDate = DateTime.Now,
                        NepaliDate = NepaliCalender.Convert.Now,
                    };
                    storeDb.GlobalErrorLogs.Add(errorLog);
                    storeDb.SaveChanges();
                }
                filterContext.Result = new RedirectToRouteResult("Default", new System.Web.Routing.RouteValueDictionary{{"controller", "Home"},
                            {"action", "Error"},
                            {"returnUrl", filterContext.HttpContext.Request.RawUrl}});
                //}
            }
            catch
            {
                filterContext.Result = new RedirectToRouteResult("Default", new System.Web.Routing.RouteValueDictionary{{"controller", "Account"},
                            {"action", "Login"},
                            {"returnUrl", filterContext.HttpContext.Request.RawUrl}});
            }
        }
    }
    public static class ErrorLogger
    {
        public static void LogException(Exception ex)
        {
            using (IdentityEntities storeDb = new IdentityEntities())
            {
                var errorLog = new GlobalErrorLog()
                {
                    Id = Guid.NewGuid().ToString(),
                    Controller = "",
                    Action = "",
                    Attribute = "",
                    Title = ex.Message,
                    Error = JsonConvert.SerializeObject(ex, Formatting.Indented,
                            new JsonSerializerSettings
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            }),
                    Resolved = false,
                    ClientIpAddress = "",
                    ClientName = "",
                    EnglishDate = DateTime.Now,
                    NepaliDate = NepaliCalender.Convert.Now
                };
                storeDb.GlobalErrorLogs.Add(errorLog);
                storeDb.SaveChanges();
            }
        }
    }
}