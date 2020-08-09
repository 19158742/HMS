using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HM_WebApi.Models
{
    public class RoomsInfo
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }

        public string RoomType { get; set; }

        public double RoomPrice { get; set; }
    }
}