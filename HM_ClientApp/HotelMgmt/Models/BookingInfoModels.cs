using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelMgmt.Models
{
    public class BookingInfoModels
    {
            public IEnumerable<tbl_TmpBookingInfo> TmpBookingInfos { get; set; }
            public IEnumerable<tbl_BookingInfo> BookingInfos { get; set; }
    }
}