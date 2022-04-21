using DMS.DAL.DatabaseContext;
using DMS.DAL.EntityModels;
using DMS.DAL.StaticHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DMS.DAL.Helpers

{
    public static class ErrorHelper
    {
        public static string GetMsg(Exception ex)
        {
            return ex.InnerException != null ? ex.InnerException.InnerException != null ? ex.InnerException.InnerException.Message : ex.InnerException.Message : ex.Message;
        }

        public static string GetMsg(DbEntityValidationException ex)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var eve in ex.EntityValidationErrors)
            {
                sb.AppendFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                foreach (var ve in eve.ValidationErrors)
                {
                    sb.AppendFormat("- Property: \"{0}\", Error: \"{1}\"",
                        ve.PropertyName, ve.ErrorMessage);
                }
            }
            return sb.ToString();
        }

        public static void LogException(Exception ex, DbEntityValidationException validationException = null, string fpath = null)
        {
            string msg = "";
            if (validationException != null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var eve in validationException.EntityValidationErrors)
                {
                    sb.AppendFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        sb.AppendFormat("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                msg = sb.ToString();
            }
            else
            {
                msg = JsonConvert.SerializeObject(ex, Formatting.Indented,
                                new JsonSerializerSettings
                                {
                                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                });
            }

            RequestContext requestContext = null;
            string controllerName = "";
            string actionName = "";
            SystemInfoForSession _ActiveUser = null;
            try
            {
                HttpContext httpContext = HttpContext.Current;

                requestContext = ((MvcHandler)httpContext.CurrentHandler).RequestContext;
                controllerName = requestContext.RouteData.GetRequiredString("controller");
                actionName = requestContext.RouteData.GetRequiredString("action");
                _ActiveUser = SessionHelper.GetSession();
            }
            catch { }

            using (var dbContext = new IdentityEntities())
            {
                var eLogger = new GlobalErrorLog()
                {
                    Action = actionName,
                    Controller = controllerName,
                    ClientIpAddress = "127.0.0.1",
                    ClientName = "",
                    UserId = 0,
                    UserName = _ActiveUser?.UserName,
                    EnglishDate = DateTime.Now,
                    NepaliDate = "",
                    Error = msg,
                    Title = (fpath != null ? "File Path: " + fpath : "") + " Error: " + ex.Message,
                    Resolved = false,
                    Attribute = "",
                    Id = Guid.NewGuid().ToString()
                };
                dbContext.GlobalErrorLogs.Add(eLogger);
                dbContext.SaveChanges();
            }

        }
        public static void LogExceptionWithSession(Exception ex)
        {
            if (ex == null) { return; }
            string msg = JsonConvert.SerializeObject(ex, Formatting.Indented,
                            new JsonSerializerSettings
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            }) + " ---- " + ex.StackTrace;

            RequestContext requestContext = null;
            string controllerName = "";
            string actionName = "";
            SystemInfoForSession _ActiveUser = null;
            try
            {
                HttpContext httpContext = HttpContext.Current;

                requestContext = ((MvcHandler)httpContext.CurrentHandler).RequestContext;
                controllerName = requestContext.RouteData.GetRequiredString("controller");
                actionName = requestContext.RouteData.GetRequiredString("action");
                _ActiveUser = null;
            }
            catch { }

            using (var dbContext = new IdentityEntities())
            {
                var eLogger = new GlobalErrorLog()
                {
                    Action = actionName,
                    Controller = controllerName,
                    ClientIpAddress = "127.0.0.1",
                    ClientName = "",
                    UserId = 0,
                    UserName = _ActiveUser?.UserName,
                    EnglishDate = DateTime.Now,
                    NepaliDate = "",
                    Error = msg,
                    Title = "",
                    Resolved = false,
                    Attribute = "",
                    Id = Guid.NewGuid().ToString()
                };
                dbContext.GlobalErrorLogs.Add(eLogger);
                dbContext.SaveChanges();
            }

        }

        public static void LogException(Exception ex)
        {
            string cPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Private", "ErrorLogs");
            if (!System.IO.Directory.Exists(cPath)) { System.IO.Directory.CreateDirectory(cPath); }
            string fPath = System.IO.Path.Combine(cPath, $"Error_{DateTime.Now.ToString("yyyyMMdd")}.txt");
            string msg = DateTime.Now + " Error: " + (ex.InnerException != null ? ex.InnerException.Message : ex.Message) + ex.StackTrace;
            System.IO.File.AppendAllText(fPath, msg);
        }
    }
}
