using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HM_WebApi.Models;

namespace HM_WebApi.Controllers
{
	public class HomeController : Controller
	{
       
        public JsonResult GetItems()
        {           
            var products = new List<Product>
            {
                new Product
                {
                    productName = "Biscuits",
                    manufacturingYear = 2018,
                    brandName="ParleG"
                },
                new Product
                {
                    productName = "Cars",
                    manufacturingYear = 2018,
                    brandName="BMW"
                },
                new Product
                {
                    productName = "Cars",
                    manufacturingYear = 2018,
                    brandName="Mercedese"
                },
                new Product
                {
                    productName = "Brush",
                    manufacturingYear = 2017,
                    brandName="Colgate"
                }

            };
            //return Ok(new { results = products });
            return Json(products, JsonRequestBehavior.AllowGet);
        }
    }
}

