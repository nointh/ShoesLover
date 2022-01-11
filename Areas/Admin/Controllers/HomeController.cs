using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesLover.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesLover.Areas.Admin.Controllers
{
    [Area("Admin")]    
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            StoreContext db = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
            ViewBag.TopCustomers = db.GetTopSalesCustomers();
            return View();
        }
        public IActionResult GetRevenueByDay()
        {
            StoreContext db = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
            var salesList = db.GetSalesByDay();
            List<object> resultList = new List<object>();
            foreach( var item in salesList)
            {
                resultList.Add(new { 
                    x = item.OrderDate.ToString("yyyy-MM-dd"),
                    y = item.Total
                });
            }
            return Json(resultList);
        }
    }
}
