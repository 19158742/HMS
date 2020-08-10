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
        [EnableCors(origins: "https://localhost:44391", headers: "*", methods: "*")]
        [HttpPost]
        [Route("PostNewBookRoom")]
        public string PostNewBookRoom([FromBody] BookRoomViewModels Bookmyroom)
        {
            using (var ctx = new hoteldbEntities())
            {
                ctx.tbl_TmpBookingInfo.Add(new tbl_TmpBookingInfo()
                {
                    room_id = Bookmyroom.RoomId,
                    room_type = Bookmyroom.RoomTpe,
                    from_dt = Convert.ToDateTime(Bookmyroom.FromDt),
                    to_dt = Convert.ToDateTime(Bookmyroom.ToDt),
                    cust_name = Bookmyroom.CustomerName,
                    total_amt = Bookmyroom.TotalAmt,
                    transactn_type = Bookmyroom.TransactionType
                });
                ctx.SaveChanges();
            }
            //tbl_TmpBookingInfo objTbl = new tbl_TmpBookingInfo();

            //objTbl.room_id = objBookRoom.RoomId;
            //objTbl.room_type = objBookRoom.RoomTpe;
            //objTbl.from_dt = dt1;
            //objTbl.to_dt = dt2;
            //objTbl.cust_name = objBookRoom.CustomerName;
            //objTbl.total_amt = objBookRoom.TotalAmt;
            //objTbl.transactn_type = objBookRoom.TransactionType;
            //_db.tbl_TmpBookingInfo.Add(objTbl);
            //_db.SaveChanges();
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
