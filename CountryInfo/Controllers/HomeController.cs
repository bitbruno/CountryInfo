using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CountryInfo.Models;

namespace CountryInfo.Controllers
{
    public class HomeController : Controller
    {
        private CountryInfoModelDataContext db = new CountryInfoModelDataContext();  
        public ActionResult Index()
        {
            //show the list of countinents in the index page.
            

            ViewBag.Message = "List of Continents";
            return View(db.Continents.ToList());
        }

        public ActionResult About()
        {
            ViewBag.About = "CountryInfo \nCHI .NET Exercise\nAuthor: Bruno Cardoso";
            return View();
        }
    }
}
