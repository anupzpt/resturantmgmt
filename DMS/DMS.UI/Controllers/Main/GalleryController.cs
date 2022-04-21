using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DMS.Controllers.Main
{
    public class GalleryController : Controller
    {
        [AllowAnonymous]

        // GET: Gallery
        public ActionResult Gallery()
        {
            return View();
        }
    }
}