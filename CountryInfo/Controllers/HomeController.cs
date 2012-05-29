using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CountryInfo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //show the list of countinents in the index page.

            return View();
        }

        public ActionResult About()
        {
            ViewBag.About = "CountryInfo \nCHI .NET Exercise\nAuthor: Bruno Cardoso";
            return View();
        }
    }
}
