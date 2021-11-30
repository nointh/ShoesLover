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
            ViewData["context"] = context;
            return View(context.GetProducts());
        }
        public IActionResult CreateProduct()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
            IEnumerable<CategoryMasterModel> allCategory = context.GetCategoryMasters();
            ViewData["category"] = allCategory.Where(item => item.SubCategoryList.Count > 0);
            ViewData["brand"] = context.GetBrands();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateProduct(Product product, string? redirectOption)
        {            
            ViewData.Model = product;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
            int[] result = context.InsertProduct(product);
            TempData["message"] = result[0] > 0 ? "Thêm sản phẩm thành công" : "Thêm sản phẩm thất bại";
            ViewData["Id"] = result[1];
            if (result[0] <= 0 )
            {
                return RedirectToAction("Index");
            }    
            if (redirectOption.Equals("continue"))
            {
                return RedirectToAction("ProductDetails", new { id = result[1] });
            }
            return RedirectToAction("Index");
        }
        // GET: ProductController/ProductDetails/5
        public ActionResult ProductDetails(int id)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
            
            return View(context.GetProductMasterData(id));
        }
        public IActionResult SetupVariant(int id)
        {
            return View();
        }
        public IActionResult CreateVariant(int id)
        {
            return View(id);
        }
    }
}
