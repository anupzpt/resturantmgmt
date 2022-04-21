using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DMS.Services;
using DMS.DAL.StaticHelper;
using DMS.DAL.DatabaseContext;
using DMS.DAL.EntityModels;

namespace DMS.Controllers
{
    public class _BaseAuthenticatedController : AsyncController
    {
        public readonly SystemInfoForSession _ActiveSession;
        public readonly GlobalErrorLog _ErroLog;

        public _BaseAuthenticatedController()
        {

            _ActiveSession = SessionHelper.GetSession();
            _ErroLog = new GlobalErrorLog();
        }

    }
}