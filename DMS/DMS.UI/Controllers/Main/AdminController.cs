using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DMS.Controllers.Main
{
    public class AdminController : Controller
    {
        [AllowAnonymous]
        // GET: Admin
        public ActionResult Adminindex()
        {
            return View();
        }
    }
}