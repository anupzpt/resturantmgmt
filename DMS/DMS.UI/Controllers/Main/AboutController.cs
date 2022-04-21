using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DMS.Controllers.Main
{
    public class AboutController : Controller
    {
        [AllowAnonymous]

        // GET: About
        public ActionResult About()
        {
            return View();
        }
    }
}