using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using DinkToPdf;
using HM_WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace HM_WebApi.Controllers
{
    public class InvoiceController : Controller
    {
        // GET: Invoice
        public FileResult Index()
        {
            return null;
        }
       // [EnableCors(origins: "http://mywebclient.azurewebsites.net", headers: "*", methods: "*")]
        public string GetItems(int invoiceNumber, string customerName, [FromUri] string[] productName, [FromUri] decimal[] productPrice,    double totalAmt, double balanceAmt, string transactionType, string siteName )
        {
            try
            {
                /*
                 https://localhost:44335/invoice/getitems?invoiceNumber=11&customerName=shraddha&productName=P1&productName=P2&productPrice=2&productPrice=32&totalAmt=43&balanceAmt=5&transactionType=cash&siteName=dsf
                 */
                var prodlist = new List<ProductInfo>();
                for (int i = 0; i < productName.Length; i++)
                {
                    var p = new ProductInfo();
                    p.productName = productName[i];
                    p.productPrice = productPrice[i];
                    prodlist.Add(p);
                }
                string str = string.Empty;
                string str2 = "";
                string str1 = "<html><head><head><style>table, th, td {border: 1px solid black;border - collapse: collapse;width: 50%;text - align: center;margin - left: 30 %;}</style></head><body><center><table><tr><td colspan = '2'><h1>" + siteName + "</h1></td></tr><tr><td colspan = '2'><h5> Customer Name: " + customerName + "</h5></td></tr><tr><td colspan = '2'><h5> Invoice Number: " + Convert.ToString(invoiceNumber) + " &nbsp; Date:" + Convert.ToString(DateTime.Now) + "</h5></td></tr><tr><td colspan = '2'><h5> Transaction Type:" + transactionType + "</h5></td></tr><tr> <td><h5>" +
                    "Product Name </h5></td><td><h5>Product Price </h5></td></tr>";
                for (int i = 0; i < prodlist.Count; i++)
                {
                    str2 += " <tr><td>" + prodlist[i].productName + "</td><td>" + Convert.ToString(prodlist[i].productPrice) + "</td></tr>";
                }
                string str3 = "<tr><td colspan = '2' ><h5> Total Amount: " + Convert.ToString(totalAmt) + "</h5></td></tr><tr><td colspan = '2'><h5> Balanced Amount: " + Convert.ToString(balanceAmt) + "</h5></td></tr></table></center></body></html>";
                str = str1 + str2 + str3;
                /* pdf
                 byte[] stream = Encoding.ASCII.GetBytes(str);
                 var result = new HttpResponseMessage(HttpStatusCode.OK)
                 {
                     Content = new ByteArrayContent(stream.ToArray())
                 };
                 result.Content.Headers.ContentDisposition =
                     new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                     {
                         FileName = "CertificationCard.pdf"
                     };
                 result.Content.Headers.ContentType =
                     new MediaTypeHeaderValue("application/pdf");
                 return result;
                */
                var response = new HttpResponseMessage();
                response.Content = new StringContent(str);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                return str;
            }
            catch(Exception e)
            {
                return null;
            }
        }
    }
}