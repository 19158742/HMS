using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HotelMgmt.Models;

namespace HotelMgmt.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ApproveRoomController : Controller
    {
        private readonly hoteldbEntities db = new hoteldbEntities();
        // GET: ApproveRoom
        public ActionResult Index()
        {
            var tables = new BookingInfoModels
            {
                TmpBookingInfos = db.tbl_TmpBookingInfo.ToList(),
                BookingInfos = db.tbl_BookingInfo.ToList()
            };
            return View(tables);
        }

        [HttpPost, ActionName("DeleteTmp")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTmp(int id)
        {
            tbl_TmpBookingInfo tbl_TmpBookingInfo = db.tbl_TmpBookingInfo.Find(id);
            db.tbl_TmpBookingInfo.Remove(tbl_TmpBookingInfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("DeleteConf")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConf(int? id)
        {
            tbl_BookingInfo tbl_BookingInfo = db.tbl_BookingInfo.Find(id);
            db.tbl_BookingInfo.Remove(tbl_BookingInfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Approve")]
        [ValidateAntiForgeryToken]
        public ActionResult Approve(int? id)
        {
            var data1 = db.tbl_TmpBookingInfo.Find(id);
            tbl_BookingInfo data2 = new tbl_BookingInfo();
            data2.from_dt = data1.from_dt;
            data2.room_id = data1.room_id;
            data2.room_type = data1.room_type;
            data2.to_dt = data1.to_dt;
            data2.total_amt = data1.total_amt;
            data2.transactn_type = data1.transactn_type;
            db.tbl_BookingInfo.Add(data2);
            db.tbl_TmpBookingInfo.Remove(data1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}