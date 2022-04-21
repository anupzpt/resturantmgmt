using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DMS.Controllers.Main
{
    public class ResmenuController : Controller
    {
        [AllowAnonymous]
        // GET: Resmenu
        public ActionResult Resmenu()
        {
            return View();
        }
    }
}