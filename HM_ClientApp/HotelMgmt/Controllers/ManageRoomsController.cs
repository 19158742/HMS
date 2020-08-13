using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HotelMgmt.Models;

namespace HotelMgmt.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManageRoomsController : Controller
    {
        private hoteldbEntities db = new hoteldbEntities();

        // GET: ManageRooms
        public ActionResult Index()
        {
            try
            {
                return View(db.tbl_RoomInfo.ToList());
            }
            catch (Exception ex)
            {
                return null;
            }
}

        // GET: ManageRooms/Details/5
        public ActionResult Details(int? id)
        {
            try 
            { 
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tbl_RoomInfo tbl_RoomInfo = db.tbl_RoomInfo.Find(id);
                if (tbl_RoomInfo == null)
                {
                    return HttpNotFound();
                }
                return View(tbl_RoomInfo);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET: ManageRooms/Create
        public ActionResult Create()
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

        // POST: ManageRooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "room_id,room_name,room_type,room_price")] tbl_RoomInfo tbl_RoomInfo)
        {
            try
            { 
            if (ModelState.IsValid)
            {
                db.tbl_RoomInfo.Add(tbl_RoomInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_RoomInfo);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET: ManageRooms/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_RoomInfo tbl_RoomInfo = db.tbl_RoomInfo.Find(id);
            if (tbl_RoomInfo == null)
            {
                return HttpNotFound();
            }
            return View(tbl_RoomInfo);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // POST: ManageRooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "room_id,room_name,room_type,room_price")] tbl_RoomInfo tbl_RoomInfo)
        {
            try { 
            if (ModelState.IsValid)
            {
                db.Entry(tbl_RoomInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_RoomInfo);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET: ManageRooms/Delete/5
        public ActionResult Delete(int? id)
        {
            try { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_RoomInfo tbl_RoomInfo = db.tbl_RoomInfo.Find(id);
            if (tbl_RoomInfo == null)
            {
                return HttpNotFound();
            }
            return View(tbl_RoomInfo);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // POST: ManageRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try { 
            tbl_RoomInfo tbl_RoomInfo = db.tbl_RoomInfo.Find(id);
            db.tbl_RoomInfo.Remove(tbl_RoomInfo);
            db.SaveChanges();
            return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    db.Dispose();
                }
                base.Dispose(disposing);
            }
            catch (Exception ex)
            {
               
            }
}
    }
}
