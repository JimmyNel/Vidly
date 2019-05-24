using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VidlyExercice1.Controllers
{
    public class RentalsController : Controller
    {
        // GET: Rentals
        [HttpGet]
        public ActionResult New()
        {
            return View();
        }
    }
}