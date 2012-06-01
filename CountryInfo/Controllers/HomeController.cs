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
        //creates a DataContext variable to handle the communications with the database
        private CountryInfoModelDataContext db = new CountryInfoModelDataContext();

        // GET: /
        public ActionResult Index()
        {
            //show the map of continents in the index page.
            ViewBag.Message = "Select a continent";
            return View(db.Continents.ToList());
        }

        // GET: /Home/InfoList/
        public ActionResult InfoList()
        {
            //show a list with all the informations stored in the database
            ViewBag.Message = "List of informations about the countries";
            return View(db.Informations.ToList());
        }

        // GET: /Home/CountryInfoList/
        public ActionResult CountryInfoList(string id = "")
        {
            //return all the informations available of the selected country
            ViewBag.Message = "List of informations about the countries";

            //select the countries that belong to the selected continent by the continent name
            var infos = from c in db.Informations where c.Country.Country_Name == id select c;
            //if the user enters in the CountryInfoList page without any parameter or the user types a id for an unexistent country return nothing
            if (id == "" || infos.Count() == 0)
            {
                return null;
            }
            return View(infos);
        }

        // GET: /Home/CountryInfo/America
        public ActionResult CountryInfo(string id = "")
        {
            ViewBag.Message = "Select a country";

            //select the countries that belong to the selected continent by the continent name
            var countries = from c in db.Countries where c.Continent.Continent_Name == id select c;
            //if the user enters in the CountrtInfo page without any parameter or the user types a id for an unexistent continent return to the main page
            if (id == "" || countries.Count() == 0)
            {
                return RedirectToAction("Index");
            }

            //pass the continent name to the view
            ViewBag.Continent = id;

            //return the list of countries of the selected continent
            return View(countries);
        }


        // POST: /Home/CountryInfo/
        //Handles the POST request to the CountryInfo page, in the post we receive the information data and save it
        [HttpPost]
        public ActionResult CountryInfo(string info_text, string selectedCountry)
        {
            //get the IP Address of the user
            string ipAddress = Request.ServerVariables["REMOTE_ADDR"];
          
            //creates a new Information object
            Information info = new Information();
            info.Information_Text = info_text;
            info.IPAddress = ipAddress;
            //Get the Country ID from the country name to store in the information object
            var countrt_id = from c in db.Countries where c.Country_Name == selectedCountry select c.Id;
            info.Country_Id = countrt_id.First();
            //Always save the current time
            info.Time = DateTime.Now;

            //Call the GetLocation method to obtain the location based on the IP Address
            info.Location = GetLocation(ipAddress);

            //Insert the information object in the database and submit the changes
            db.Informations.InsertOnSubmit(info);
            db.SubmitChanges();

            return null;
        }

        // GET: /Home/About/
        public ActionResult About()
        {
            //show the aobut view file
            return View();
        }


        private string GetLocation(string ipAddress)
        {
            //creates a web client variable to make the web request
            var client = new WebClient();
            //format the webservice string
            string webService = string.Format("http://freegeoip.net/xml/{0}", ipAddress);
            //creates a XElement based on the string downloaded from the webservice
            XElement xml = XElement.Parse(client.DownloadString(webService));
            //select the country from the XML
            var qCountry = from c in xml.Elements("CountryName") select c;
            //select the city from the XML
            var qCity = from c in xml.Elements("City") select c;
            //return the string  "Country - City"
            return qCountry.First().Value + " - " + qCity.First().Value;
        }
    }
}
