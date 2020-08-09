using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelMgmt.Models;
namespace HotelMgmt.Controllers
{
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
            var list = new SelectList(new[]
                {
                    new {id=1,name="Cash" },
                    new {id=2,name="Online" }
                },"id","name",1);

            ViewData["tranlist"] = list;
            return View();
        }

        public ActionResult ViewPhotos()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BookRoomViewModels objBookRoom)
        {
            //if (ModelState.IsValid)
            //{
                tbl_TmpBookingInfo objTbl = new tbl_TmpBookingInfo();
                objTbl.room_id = objBookRoom.RoomId;
                objTbl.room_type = objBookRoom.RoomTpe;
                objTbl.from_dt = Convert.ToDateTime(objBookRoom.FromDt);
                objTbl.to_dt = Convert.ToDateTime(objBookRoom.ToDt);
                objTbl.cust_name = objBookRoom.CustomerName;
                objTbl.total_amt = objBookRoom.TotalAmt;
                objTbl.transactn_type = objBookRoom.TransactionType;
                _db.tbl_TmpBookingInfo.Add(objTbl);
                _db.SaveChanges();
                
            //}
            return View("Details",objTbl);
        }

        //    public string CheckAvail() {

        //    DateTime fromdate = Convert.ToDateTime((HttpContext.Request.Url).ToString().Split('?')[1].Split('&')[0].Split('=')[1]);
        //    DateTime todate = Convert.ToDateTime((HttpContext.Request.Url).ToString().Split('?')[1].Split('&')[1].Split('=')[1]);
        //    string roomtype = Convert.ToString(ViewBag.roomtype);
        //    bool ifuser = true;

        //    if (ifuser == false)

        //    {

        //        return "Available";

        //    }

        //    if (ifuser == true)

        //    {

        //        return "Not Available";

        //    }

        //    return "";
        //}
    }
}