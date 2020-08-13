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
using HotelApi.Models;
using Microsoft.AspNetCore.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Configuration;
using System.Web.UI;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;
using System.Drawing;

namespace HotelApi
{
    public class InvoiceController : ApiController
    {
        // GET: Invoice
        public System.Web.Mvc.FileResult Index()
        {
            return null;
        }
        [EnableCors(origins: "http://hotelapi20200806072002.azurewebsites.net", headers: "*", methods: "*")]
        public HttpResponseMessage GetItems(int invoiceNumber, string customerName, [FromUri] string[] productName, [FromUri] decimal[] productPrice,    double totalAmt, double balanceAmt, string transactionType, string siteName )
        {
            try
            {
                /*
               Below code returns html response
                 https://localhost:44335/invoice/getitems?invoiceNumber=11&customerName=shraddha&productName=P1&productName=P2&productPrice=2&productPrice=32&totalAmt=43&balanceAmt=5&transactionType=cash&siteName=dsf 
                 
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
                
                var response = new HttpResponseMessage();
                response.Content = new StringContent(str);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                return str;
                */
                // This code is for export Database data to PDF file
                string fileName = Guid.NewGuid() + ".pdf";
                //string filePath = Path.Combine(ConfigurationManager.AppSettings["PDFPath"], fileName);
                // CreatePDF(fileName);

                //string reqBook = filePath; // format.ToLower() == "pdf" ? bookPath_Pdf : (format.ToLower() == "xls" ? bookPath_xls : (format.ToLower() == "doc" ? bookPath_doc : bookPath_zip));
                //string bookName = fileName;
                //converting Pdf file into bytes array  
                //var dataBytes = System.IO.File.ReadAllBytes(filePath);
                //adding bytes to memory stream   
                //var dataStream = new MemoryStream(dataBytes);
                Stream stream = new MemoryStream(CreatePDF());
                HttpResponseMessage httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK);
                //HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                //httpResponseMessage.Content = new StreamContent(dataStream);
                httpResponseMessage.Content = new StreamContent(stream);
                httpResponseMessage.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                httpResponseMessage.Content.Headers.ContentDisposition.FileName = fileName;
                httpResponseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

                return httpResponseMessage;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        protected byte[] CreatePDF()
        {
           
            var data = Request.RequestUri.ParseQueryString();
            Document doc = new Document(PageSize.A4, 2, 2, 2, 2);

            var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
            boldFont.SetColor(0, 0, 204);
            var phrase = new Phrase();
            Chunk c = new Chunk("" + data[7], boldFont);
            c.SetUnderline(2, -3);
            phrase.Add(c);
            // Create paragraph for show in PDF file header
            Paragraph p = new Paragraph(phrase);
            p.Alignment = Element.ALIGN_CENTER;
            
            //p.set("center");

            Paragraph p1 = new Paragraph("Invoice Number - " + data[0]);
            //p1.SetAlignment("center");

            Paragraph p2 = new Paragraph("Customer Name - " + data[1]);
            //p2.SetAlignment("center");
           
            try
            {
                //PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
                MemoryStream ms = new MemoryStream();
                PdfWriter writer = PdfWriter.GetInstance(doc, ms);

                PdfPTable pdfTab = new PdfPTable(2); // here 2 is no of column
                pdfTab.HorizontalAlignment = 1; // 0- Left, 1- Center, 2- right
                pdfTab.SpacingBefore = 20f;
                pdfTab.SpacingAfter = 20f;

                pdfTab.AddCell("Product Name");
                pdfTab.AddCell("Price");
                for (int i = 0; i < data[2].Split(',').Length; i++)
                {
                    pdfTab.AddCell(data[2].Split(',')[i]);
                    pdfTab.AddCell(data[3].Split(',')[i]);
                }

                Paragraph p3= new Paragraph("Transaction Type - " + data[6]);
               // p3.SetAlignment("left");

                Paragraph p4 = new Paragraph("Total Amount - " + data[4]);
                //p4.SetAlignment("left");

                Paragraph p5 = new Paragraph("Balance Amount - " + data[5]);
                //p5.SetAlignment("left");

               
                doc.Open();
                doc.Add(p);
                doc.Add(p1);
                doc.Add(p2);
                doc.Add(pdfTab);
                doc.Add(p3);
                doc.Add(p4);
                doc.Add(p5);
                doc.Close();

                //byte[] content = File.ReadAllBytes(filePath);
                byte[] content = ms.ToArray();
                HttpContext context = HttpContext.Current;

                context.Response.BinaryWrite(content);
                context.Response.ContentType = "application/pdf";
                //context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                context.Response.AppendHeader("Content-Length", content.Length.ToString());
                context.Response.OutputStream.Write(content, 0, (int)content.Length);
                context.Response.End();
                return content;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                doc.Close();
            }
        }
    }
}