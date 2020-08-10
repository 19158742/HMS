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
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44391/bookmyroom/postnewbookroom");

                
            //HTTP POST
            var postTask = client.PostAsJsonAsync<BookRoomViewModels>("PostNewBookRoom", objBookRoom);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return View("Details", objBookRoom);
                }
            }
             return RedirectToAction("Index","Home");
           
        }

        public FileStreamResult DownloadFile(string requestUrl)
        {
            string serverUrl = "https://localhost:44391/invoice/getitems?invoiceNumber=11&customerName=shraddha&productName=P1&productName=P2&productPrice=2&productPrice=32&totalAmt=43&balanceAmt=5&transactionType=cash&siteName=dsf";
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