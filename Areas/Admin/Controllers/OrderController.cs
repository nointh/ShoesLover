using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesLover.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoesLover.Models;

namespace ShoesLover.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;

            return View(context.GetAllOrder());
           
        }

        public IActionResult Edit(int id)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;       
            ViewBag.ShowInfoOrderDetail = context.GetInfoOrderDetail(id);
            ViewBag.Order_ID = id;
            ViewBag.Old_Order_Date = context.GetOldOrderDate(id);
            return View(context.GetInfoCustomer(id));
        }
        public IActionResult OrdersOfUser(int id)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            ViewBag.CurrentUser = context.GetUsers().Where(u => u.ID == id).FirstOrDefault();
            return View(context.GetAllOrder().Where(order => order.UID == id));
        }

       
    }
}
