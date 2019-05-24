using System;
using System.Web.Mvc;

namespace VidlyExercice1.Controllers
{
    // Apply filter/annotation to allow the below pages to anonymous user (not authenticated)
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            throw new Exception();

            //ViewBag.Message = "Your application description page.";

            //return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}