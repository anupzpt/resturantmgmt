using DMS.DAL.DatabaseContext;
using DMS.DAL.EntityModels;
using DMS.DAL.Helpers;
using DMS.DAL.StaticHelper;
using DMS.UI;
using DMS.UI.App_Start;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DMS
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.Config();
            UserManagement.IdentitySeed();
            UnityConfig.RegisterTypes(UnityConfig.Container);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            HttpContext httpContext = HttpContext.Current;
            if (httpContext != null)
            {
                RequestContext requestContext = ((MvcHandler)httpContext.CurrentHandler).RequestContext;
                string controllerName = requestContext.RouteData.GetRequiredString("controller");
                string actionName = requestContext.RouteData.GetRequiredString("action");
                var _ActiveUser = SessionHelper.GetSession();
                string errMsg = "";

                Exception exception = Server.GetLastError();
                if (exception != null)
                {
                    //log the error
                    errMsg = ErrorHelper.GetMsg(exception);
                    using (var dbContext = new IdentityEntities())
                    {
                        var eLogger = new GlobalErrorLog()
                        {
                            Action = actionName,
                            Controller = controllerName,
                            ClientIpAddress = "127.0.0.1",
                            ClientName = Request.ServerVariables[""],
                            UserId = 0,
                            UserName = _ActiveUser?.UserName,
                            EnglishDate = DateTime.Now,
                            NepaliDate = "",
                            Error = errMsg,
                            Title = "",
                            Resolved = false,
                            Attribute = "",
                            Id = Guid.NewGuid().ToString()
                        };
                        dbContext.GlobalErrorLogs.Add(eLogger);
                        dbContext.SaveChanges();
                    }
                }

                /* When the request is ajax the system can automatically handle a mistake with a JSON response. 
                   Then overwrites the default response */
                if (requestContext.HttpContext.Request.IsAjaxRequest())
                {
                    httpContext.Response.Clear();
                    IControllerFactory factory = ControllerBuilder.Current.GetControllerFactory();
                    IController controller = factory.CreateController(requestContext, controllerName);
                    ControllerContext controllerContext = new ControllerContext(requestContext, (ControllerBase)controller);

                    JsonResult jsonResult = new JsonResult
                    {
                        Data = new { success = false, serverError = "500" },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                    jsonResult.ExecuteResult(controllerContext);
                    httpContext.Response.End();
                }
                else
                {
                    FlashBag.setMessage(false, errMsg);
                    httpContext.Response.Redirect("~/Error", false);
                }
            }

        }
    }
}