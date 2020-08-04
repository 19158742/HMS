using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelMgmt.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "The main responsibility of this application is to help customers with planning and booking reservations for a hotel room. Application will help our customers to plan out the duration of their stay and then send booking confirmation to guests after processing payments.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Online Website";

            return View();
        }
    }
}