using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DMS.Controllers.Main
{
    public class ContactController : Controller
    {
        [AllowAnonymous]
        // GET: Contact
        public ActionResult Contact()
        {
            return View();
        }
    }
}