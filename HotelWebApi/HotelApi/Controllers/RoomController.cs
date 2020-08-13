using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HotelApi.Models;

using Org.BouncyCastle.Math.EC.Rfc7748;
using System.Web.Http.Cors;
using System.Threading;

namespace HotelApi.Controllers
{
    public class RoomController : ApiController
    {
        //https://localhost:44391/Room/Get?roomtype=Single&dtFrom=1/1/2019&dtTo=2/1/2019
        [EnableCors(origins: "http://hotelapi20200806072002.azurewebsites.net", headers: "*", methods: "*")]
        public HttpResponseMessage Get(string roomType,string dtFrom,string dtTo)
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            DateTime dtf = Convert.ToDateTime(dtFrom);
            DateTime dtt = Convert.ToDateTime(dtTo);
            List<tbl_RoomInfo> roomList1 = new List<tbl_RoomInfo>();
            List<tbl_RoomInfo> roomList2 = new List<tbl_RoomInfo>();
            using (hoteldbEntities dc = new hoteldbEntities())
            {
                var rid = (from r in dc.tbl_RoomInfo
                             join rb in dc.tbl_BookingInfo on r.room_id equals rb.room_id
                             where r.room_type == roomType && (rb.from_dt >= dtf && rb.to_dt <= dtt) && r.room_id == rb.room_id
                             select rb.room_id).Distinct().ToArray();
                
                roomList2 = dc.tbl_RoomInfo.Where(x => x.room_type == roomType).ToList();
                roomList1 = roomList2.Where(x => !rid.Contains(x.room_id)).ToList();  
                HttpResponseMessage response;
                response = Request.CreateResponse(HttpStatusCode.OK, roomList1);
                return response;
            }
        }

        [EnableCors(origins: "http://hotelapi20200806072002.azurewebsites.net", headers: "*", methods: "*")]
        public HttpResponseMessage GetRoomPrice(int roomid)
        {
            using (hoteldbEntities dc = new hoteldbEntities())
            {
                var rid = (from r in dc.tbl_RoomInfo
                           where r.room_id == roomid 
                           select r.room_price).Single();
                HttpResponseMessage response;
                response = Request.CreateResponse(HttpStatusCode.OK, Convert.ToDouble(rid));
                return response;
            }
        }

    }
}
