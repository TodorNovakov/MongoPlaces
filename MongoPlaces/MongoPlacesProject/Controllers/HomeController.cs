using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MongoPlacesProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Map()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult User()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Place()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
