using Microsoft.AspNetCore.Mvc;
using ShoesLover.Data;
using ShoesLover.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesLover.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
            ViewData["Context"] = context;
            return View(context.GetProducts());
        }
        public IActionResult EnterProduct()
        {
            return View();
        }
        public IActionResult InsertProduct(Product product)
        {
            ViewData.Model = product;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
            int[] result = context.InsertProduct(product);
            ViewData["message"] = result[0] > 0 ? "Them san pham thanh cong" : "Them san pham that bai";
            ViewData["Id"] = result[1];
            return View();
        }
        public IActionResult SetupVariant(int id)
        {
            return View();
        }
        public IActionResult InsertVariant(int id)
        {
            return View(id);
        }
    }
}
