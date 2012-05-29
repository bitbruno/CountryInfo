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


        public ActionResult CountryInfo(int id = 0)
        {
            ViewBag.Message = "List of Countries";
            ViewBag.continentID = id;

            //select the countries that belong to the selected country
            var countries = from c in db.Countries where c.Continent_ID == id select c;

            if (id == 0 || countries.Count() == 0)
            {
                return RedirectToAction("Index");
            }

            //show a list of continents
            return View(countries);
        }



        public ActionResult About()
        {
            ViewBag.About = "CountryInfo \nCHI .NET Exercise\nAuthor: Bruno Cardoso";
            return View();
        }
    }
}
