using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
            //string serverUrl = "https://localhost:44391/Bookmyroom/GetNewBookRoomResponse?Bookmyroom="+objBookRoom;
            string serverUrl = "https://hotelapi20200806072002.azurewebsites.net/Bookmyroom/GetNewBookRoomResponse?roomid=" + objBookRoom.RoomId + "&roomtype="+objBookRoom.RoomTpe+"&frmdt="+objBookRoom.FromDt+"&todt="+objBookRoom.ToDt+"&custname="+objBookRoom.CustomerName+"&totalamt="+objBookRoom.TotalAmt+"&trantype="+objBookRoom.TransactionType;
            var client = new System.Net.WebClient();
            client.UseDefaultCredentials = true;
            client.Credentials = System.Net.CredentialCache.DefaultCredentials;
            client.Headers.Add("Content-Type", "text/plain");
            string result = client.DownloadString(serverUrl);
            if(result.Contains("yes"))
            {
                return View("Details", objBookRoom);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("https://localhost:44391/bookmyroom/postnewbookroom");


            ////HTTP POST
            //var postTask = client.PostAsJsonAsync<BookRoomViewModels>("PostNewBookRoom", objBookRoom);
            //    postTask.Wait();
            //    var result = postTask.Result;
            //    if (result.IsSuccessStatusCode)
            //    {
            //        return View("Details", objBookRoom);
            //    }
            //}
            //return RedirectToAction("Index","Home");
           
        }

       
        public FileStreamResult DownloadFile(BookRoomViewModels objBookRoom)
        {
            int lastInvoiceId = _db.tbl_TmpBookingInfo.Max(item => item.tmp_booking_id);
             var obj = (from s in _db.tbl_TmpBookingInfo
                    select new
                    {
                        cust_name = s.cust_name,
                        invoice_id = s.tmp_booking_id,
                        prod_name = s.room_type,
                        prod_price =s.total_amt,
                        total_amt = s.total_amt,
                        bal_amt = 0,
                        tran_type = s.transactn_type,
                        site_name = "Hotel Booking Site"
                    }).First();
            string serverUrl = "https://hotelapi20200806072002.azurewebsites.net/invoice/getitems?invoiceNumber=" + lastInvoiceId +"&customerName="+obj.cust_name+"&productName="+obj.prod_name +" Room"+"&productPrice="+obj.prod_price+"&totalAmt="+obj.total_amt+"&balanceAmt="+obj.bal_amt+"&transactionType="+obj.tran_type+"&siteName=" + obj.site_name;
            //string serverUrl = "https://localhost:44391/invoice/getitems?invoiceNumber=11&customerName=shraddha&productName=P1&productName=P2&productPrice=2&productPrice=32&totalAmt=43&balanceAmt=5&transactionType=cash&siteName=dsf";
            //html response start
            //var client = new System.Net.WebClient();
            //client.Headers.Add("Content-Type", "text/html");
            //byte[] b = client.DownloadData(serverUrl);
            //string x = System.Text.Encoding.Default.GetString(b);
            //// System.IO.File.WriteAllBytes(@"d:\somepath.doc", b);
            //// return client.DownloadData(serverUrl);
            //return base.Content(x);
            //html response end
            var client = new System.Net.WebClient();
            client.UseDefaultCredentials = true;
            client.Credentials = System.Net.CredentialCache.DefaultCredentials;
            client.Headers.Add("Content-Type", "text/pdf");
            byte[] b = client.DownloadData(serverUrl);
            MemoryStream workStream = new MemoryStream();
            workStream.Write(b, 0, b.Length);
            workStream.Position = 0;
            return new FileStreamResult(workStream, "application/pdf");

        }
    }
}