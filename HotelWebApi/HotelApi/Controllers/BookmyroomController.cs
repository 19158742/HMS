using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using HotelApi.Models;
using HotelMgmt.Models;

namespace HotelApi.Controllers
{
    public class BookmyroomController : ApiController
    {
        // GET: api/Bookmyroom
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Bookmyroom/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Bookmyroom
        [EnableCors(origins: "http://hotelapi20200806072002.azurewebsites.net", headers: "*", methods: "*")]
        public string GetNewBookRoomResponse([FromUri] int roomid,string roomtype,string frmdt,string todt,string custname,string totalamt,string trantype)
        {
            //using (var ctx = new hoteldbEntities())
            //{
            //ctx.tbl_TmpBookingInfo.Add(new tbl_TmpBookingInfo()
            //{
            //    room_id = Bookmyroom.room_id,
            //    room_type = Bookmyroom.room_type,
            //    from_dt = Convert.ToDateTime(Bookmyroom.from_dt),
            //    to_dt = Convert.ToDateTime(Bookmyroom.to_dt),
            //    cust_name = Bookmyroom.cust_name,
            //    total_amt = Bookmyroom.total_amt,
            //    transactn_type = Bookmyroom.transactn_type
            //});
            //ctx.SaveChanges();
            hoteldbEntities _db = new hoteldbEntities();
            tbl_TmpBookingInfo objTbl = new tbl_TmpBookingInfo();
            objTbl.room_id = roomid;
            objTbl.room_type = roomtype;
            objTbl.from_dt = Convert.ToDateTime(frmdt);
            objTbl.to_dt = Convert.ToDateTime(todt);
            objTbl.cust_name = custname;
            objTbl.total_amt = Convert.ToDouble(totalamt);
            objTbl.transactn_type = trantype;
            _db.tbl_TmpBookingInfo.Add(objTbl);
            _db.SaveChanges();
            //}

            // return Ok();
            return "yes";
        }

        // PUT: api/Bookmyroom/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Bookmyroom/5
        public void Delete(int id)
        {
        }
    }
}
