using Microsoft.AspNetCore.Mvc;
using ShoesLover.Data;
using ShoesLover.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesLover.Controllers
{
    public class sizeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LietKeSize()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            return View(context.GetSizes());
        }

        public IActionResult FilterTheoSize()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            return View(context.GetSizes());
        }
        public JsonResult GetSizeById(int color_id, int size_id, int product_id)
        {
            IEnumerable<ProductQuantityBySize> size;
            try
            {
                StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
                size = context.GetSizeID(color_id, size_id, product_id);
            }
            catch
            {
                return Json(new { err = "error" });
            }
            return Json(size);
        }
    }
}
