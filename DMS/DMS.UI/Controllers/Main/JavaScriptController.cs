using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ORMPrabhu.Controllers
{
    public class JavaScriptController : Controller
    {
        // GET: JavaScript
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult Config()
        {
            return PartialView();
        }
    }
}