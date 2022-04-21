using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DMS.Controllers.Main
{
    public class BlogController : Controller
    {
        [AllowAnonymous]
        // GET: Blog
        public ActionResult Blog()
        {
            return View();
        }
        [AllowAnonymous]

        public ActionResult Blogdetail()
        {
            return View();
        }
    }
}