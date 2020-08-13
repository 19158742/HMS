using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using HotelMgmt.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HotelMgmt.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Admin()
        {
            try
            {
                string apiUri = Url.HttpRouteUrl("DefaultApi", new { controller = "admin", });
                ViewBag.ApiUrl = new Uri(Request.Url, apiUri).AbsoluteUri.ToString();

                return View();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public ActionResult Index()
        {
            try { 
                return View();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ActionResult About()
        {
            try
            {
                ViewBag.Message = "The main responsibility of this application is to help customers with planning and booking reservations for a hotel room. Application will help our customers to plan out the duration of their stay and then send booking confirmation to guests after processing payments.";

                return View();
            }
            catch (Exception ex)
            {
                return null;
            }
}

        public ActionResult Contact()
        {
            try
            {
                ViewBag.Message = "Online Website";

                return View();
            }                        
            catch (Exception ex)
            {
                return null;
            }
}
        public ActionResult Weather()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return null;
            }
}

        public ActionResult Sports()
        {
            try
            {
                string url = string.Format("https://sportsrest.azurewebsites.net/api/sports/listall");
                using (WebClient client = new WebClient())
                {
                    string splayed = string.Empty;
                    string json = client.DownloadString(url);
                    for (int i = 0; i < (System.Web.Helpers.Json.Decode(json)).Length; i++)
                    {
                        if (Convert.ToString(System.Web.Helpers.Json.Decode(json)[i].country) == "Ireland")
                        {
                            splayed = System.Web.Helpers.Json.Decode(json)[i].sportsplayed;
                        }
                    }
                    ViewBag.Message = splayed;
                    //dynamic sportsInfo = JObject.Parse(json);
                }
                return View();
            }
            catch (Exception ex)
            {
                return null;
            }
}

        public string WeatherDetail()
        {
            try
            {
                //Assign API KEY which received from OPENWEATHERMAP.ORG  
                string appId = "295dfee9118233e095540774ef2eb070";

                //API path with CITY parameter and other parameters.  
                string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&cnt=1&APPID={1}", "Dublin", appId);

                using (WebClient client = new WebClient())
                {
                    string json = client.DownloadString(url);

                    //Converting to OBJECT from JSON string.  
                    //RootObject weatherInfo = (new JavaScriptSerializer()).Deserialize<RootObject>(json);
                    //RootObject weatherInfo = JsonConvert.DeserializeObject<RootObject>(json);
                    dynamic weatherInfo = JObject.Parse(json);
                    //Special VIEWMODEL design to send only required fields not all fields which received from   
                    //www.openweathermap.org api  
                    ResultViewModel rslt = new ResultViewModel();

                    rslt.Country = weatherInfo.sys.country;
                    rslt.City = weatherInfo.name;
                    rslt.Lat = Convert.ToString(weatherInfo.coord.lat);
                    rslt.Lon = Convert.ToString(weatherInfo.coord.lon);
                    rslt.Description = weatherInfo.weather[0].description;
                    rslt.Humidity = Convert.ToString(weatherInfo.main.humidity);
                    rslt.Temp = Convert.ToString(weatherInfo.main.temp);
                    rslt.TempFeelsLike = Convert.ToString(weatherInfo.main.feels_like);
                    rslt.TempMax = Convert.ToString(weatherInfo.main.temp_max);
                    rslt.TempMin = Convert.ToString(weatherInfo.main.temp_min);
                    rslt.WeatherIcon = weatherInfo.weather[0].icon;

                    //Converting OBJECT to JSON String   
                    var jsonstring = new JavaScriptSerializer().Serialize(rslt);

                    //Return JSON string.  
                    return jsonstring;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        }
    }
