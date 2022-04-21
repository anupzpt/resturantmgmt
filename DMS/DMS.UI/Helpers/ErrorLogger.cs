using DMS.DAL.DatabaseContext;
using DMS.DAL.EntityModels;
using DMS.DAL.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMS.Helpers
{
    public static class ErrorLogger
    {
        public static void LogException(Exception ex)
        {
            try
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
                        Error = ErrorHelper.GetMsg(ex) + " " + ex.StackTrace,
                        Resolved = false,
                        ClientIpAddress = "",
                        ClientName = "",
                        EnglishDate = DateTime.Now,
                        NepaliDate = NepaliCalender.Convert.ToNepali(DateTime.Now),
                    };
                    storeDb.GlobalErrorLogs.Add(errorLog);
                    storeDb.SaveChanges();
                }
            }
            catch (Exception e)
            {
                //throw new Exception(ErrorHelper.GetMsg(ex));
            }
        }
    }
}