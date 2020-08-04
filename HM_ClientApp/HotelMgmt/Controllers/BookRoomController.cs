using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelMgmt.Controllers
{
    public class BookRoomController : Controller
    {
        // GET: BookRoom
        public ActionResult Index(string roomtype)
        {
            ViewBag.roomtype = roomtype;
            return View();
        }

        public string CheckAvail() {

            DateTime fromdate = Convert.ToDateTime((HttpContext.Request.Url).ToString().Split('?')[1].Split('&')[0].Split('=')[1]);
            DateTime todate = Convert.ToDateTime((HttpContext.Request.Url).ToString().Split('?')[1].Split('&')[1].Split('=')[1]);
            bool ifuser = true;

            if (ifuser == false)

            {

                return "Available";

            }

            if (ifuser == true)

            {

                return "Not Available";

            }

            return "";
        }
    }
}