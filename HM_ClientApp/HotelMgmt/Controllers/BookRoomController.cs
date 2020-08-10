using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using HotelMgmt.Models;
namespace HotelMgmt.Controllers
{
    [Authorize]
    public class BookRoomController : Controller
    {
        hoteldbEntities _db;

        public BookRoomController() {
            _db = new hoteldbEntities();
        }
        // GET: BookRoom
        public ActionResult Index(string roomtype)
        {
            ViewBag.roomtype = roomtype;
            return View();
        }

        [AllowAnonymous]
        public ActionResult ViewPhotos()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BookRoomViewModels objBookRoom)
        {
            DateTime dt1 = DateTime.ParseExact(objBookRoom.FromDt, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime dt2 = DateTime.ParseExact(objBookRoom.ToDt, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            objBookRoom.FromDt =Convert.ToString(dt1);
            objBookRoom.ToDt = Convert.ToString(dt2);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44391/Bookmyroom");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<BookRoomViewModels>("Bookmyroom", objBookRoom);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return View("Details", objBookRoom);
                }
            }
            return RedirectToAction("Index","Home");
        }      

       
    }
}