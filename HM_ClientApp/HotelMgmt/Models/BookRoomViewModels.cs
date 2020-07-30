using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelMgmt.Models
{
    public class BookRoomViewModels
    {
        public int BookingId { get; set; }
        public int RoomId { get; set; }
        
        [DisplayName("Room Type")]
        public string RoomTpe { get; set; }
        
        [DisplayName("From")]
        [DisplayFormat(ApplyFormatInEditMode =true,DataFormatString ="0:dd/MMM/yyyy")]
        public DateTime FromDt { get; set; }
        
        [DisplayName("To")] 
        public DateTime ToDt { get; set; }
        
        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }

        [DisplayName("Total Amount")]
        public double TotalAmt { get; set; }

        [DisplayName("Transaction Type")]
        public string TransactionType { get; set; }
    }
}