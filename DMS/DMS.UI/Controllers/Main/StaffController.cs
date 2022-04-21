using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DMS.Controllers.Main
{
    public class StaffController : Controller
    {
        [AllowAnonymous]
        // GET: Staff
        public ActionResult Staff()
        {
            return View();
        }
    }
}