using Microsoft.Owin;
using Owin;
using System;
using Hangfire;
using Hangfire.SqlServer;
using System.Diagnostics;
using DMS.DAL.DatabaseContext;
using DMS.CustomAttributes;

[assembly: OwinStartupAttribute(typeof(DMS.Startup))]
namespace DMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }


    }
}
