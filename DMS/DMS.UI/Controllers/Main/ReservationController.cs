using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DMS.Controllers.Main
{
    public class ReservationController : Controller
    {
        [AllowAnonymous]

        // GET: Reservation
        public ActionResult Reservation()
        {
            return View();
        }
    }
}