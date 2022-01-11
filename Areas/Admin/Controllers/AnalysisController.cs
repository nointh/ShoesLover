using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoesLover.Data;
using ShoesLover.Models;
using Microsoft.AspNetCore.Authorization;

namespace ShoesLover.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AnalysisController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public IActionResult SoGiay()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
            return View(context.SoLuongGiay());
        }
        public IActionResult DoanhThu()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
            return View(context.DoanhThu());
        }
        public IActionResult GetRevenueByMonth()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
            var list = context.DoanhThu();
            var labels = list.Select(item => item.GetType().GetProperty("year").GetValue(item, null)).ToArray();
            var value = list.Select(item => item.GetType().GetProperty("tong").GetValue(item, null)).ToArray();
            List<object> result = new List<object>();
            result.Add(labels);
            result.Add(value);
            return Json(result);
        }
        public IActionResult GetStockQuantity()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
            var list = context.SoLuongGiay();
            var labels = list.Select(item => item.GetType().GetProperty("tengiay").GetValue(item, null)).ToArray();
            var value = list.Select(item => item.GetType().GetProperty("soluong").GetValue(item, null)).ToArray();
            List<object> result = new List<object>();
            result.Add(labels);
            result.Add(value);
            return Json(result);
        }
        public IActionResult LietKeSubCategory()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
            IEnumerable<CategoryMasterModel> allCategory = context.GetCategoryMasters();
            ViewData["category"] = allCategory.Where(item => item.SubCategoryList.Count > 0);
            return View(context.GetSubCategories());
        }
        public IActionResult LietKeSoGiay(SubCategory sc)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
            return View(context.GetProducts(sc.Id));
        }
        public IActionResult Top5BestSeller()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
            return View(context.Top5BestSeller());
        }

        public IActionResult GetBestSellerProduct()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
            var list = context.TopBestSeller();
            var labels = list.Select(item => item.GetType().GetProperty("name").GetValue(item, null)).ToArray();
            var value = list.Select(item => item.GetType().GetProperty("all").GetValue(item, null)).ToArray();
            List<object> result = new List<object>();
            result.Add(labels);
            result.Add(value);
            return Json(result);
        }
    }
}
