
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ThamelRemit.Controllers.ErrorLog
{
    public class ErrorController : Controller
    {
        public ErrorController()
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            
            return View();
        }

    }
}