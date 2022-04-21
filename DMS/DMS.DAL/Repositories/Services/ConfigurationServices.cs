using DMS.DAL;
using DMS.DAL.DatabaseContext;
using DMS.DAL.StaticHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMS
{
    public class ConfigurationServices
    {
        MainEntities db;
        SystemInfoForSession _ActiveSession;

        public ConfigurationServices(MainEntities _db)
        {
            db = _db;
            _ActiveSession = SessionHelper.GetSession();
        }

    }
}