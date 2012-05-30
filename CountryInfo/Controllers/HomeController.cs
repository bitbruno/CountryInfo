using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CountryInfo.Models;
using System.Net;
using System.Xml.Linq;

namespace CountryInfo.Controllers
{
    public class HomeController : Controller
    {
        private CountryInfoModelDataContext db = new CountryInfoModelDataContext();

        // GET: /
        public ActionResult Index()
        {
            //show the list of countinents in the index page.
            ViewBag.Message = "List of Continents";
            return View(db.Continents.ToList());
        }

        // GET: /Home/InfoList/
        public ActionResult InfoList()
        {
            ViewBag.Message = "List of informations about the countries";
            return View(db.Informations.ToList());
        }

        /*
        // GET: /Home/CountryInfo/3
        public ActionResult CountryInfo(int id = 0)
        {
            ViewBag.Message = "List of Countries";
            ViewBag.continentID = id;

            //select the countries that belong to the selected continent
            var countries = from c in db.Countries where c.Continent_ID == id select c;


            //if the user enters in the CountrtInfo page without any parameter or the user types a id for an unexistent continent return to the main page
            if (id == 0 || countries.Count() == 0)
            {
                return RedirectToAction("Index");
            }

            //show a list of continents
            return View(countries);
        }
        */
        // GET: /Home/CountryInfo/3
        public ActionResult CountryInfo(string id = "")
        {
            ViewBag.Message = "List of Countries";

            //select the countries that belong to the selected continent
            var countries = from c in db.Countries where c.Continent.Continent_Name == id select c;


            //if the user enters in the CountrtInfo page without any parameter or the user types a id for an unexistent continent return to the main page
            if (id == "" || countries.Count() == 0)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Continent = id;


            //show a list of continents
            return View(countries);
        }


        // POST: /Home/CountryInfo/

        [HttpPost]
        public ActionResult CountryInfo(string info_text, int selectedCountry)
        {
            string ipAddress = Request.ServerVariables["REMOTE_ADDR"];
          
            Information info = new Information();
            info.Information_Text = info_text;
            info.IPAddress = ipAddress;
            info.Country_Id = selectedCountry;
            info.Time = DateTime.Now;
            info.Location = GetLocation(ipAddress);


            db.Informations.InsertOnSubmit(info);
            db.SubmitChanges();

            return RedirectToAction("CountryInfo");
        }

        // GET: /Home/About/
        public ActionResult About()
        {
            ViewBag.About = "CountryInfo \nCHI .NET Exercise\nAuthor: Bruno Cardoso";
            return View();
        }


        private string GetLocation(string ipAddress)
        {
            var client = new WebClient();
            string webService = string.Format("http://freegeoip.net/xml/{0}", ipAddress);
            XElement xml = XElement.Parse(client.DownloadString(webService));
            var qCountry = from c in xml.Elements("CountryName") select c;
            var qCity = from c in xml.Elements("City") select c;
            return qCountry.First().Value + " - " + qCity.First().Value;
        }
    }
}
