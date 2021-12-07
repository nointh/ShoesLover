using Microsoft.AspNetCore.Mvc;
using ShoesLover.Data;
using ShoesLover.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesLover.Controllers
{
    public class productController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Lietkesp()
        {
             StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            return View(context.GetProducts());
        }

        public IActionResult SanPhamCoMau()
        {

            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            return View(context.GetColorOfProduct());
        }
        public IActionResult TimSanPham()
        {
            return View();

        }
        public IActionResult TimSanPhamTheoTen(string keyword)
        {
            int count;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            return View( context.TimSanPhamTheoTen(keyword));
            if (count > 0)
                ViewData["thongbao"] = "Tìm thấy";
            else
                ViewData["thongbao"] = "Tìm không thấy";
            return View();
        }

        public IActionResult LietKeSanPhamTheoColor( Color c)
        {

            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;

            return View(context.GetProducts(c.Id));


            

        }
        public IActionResult FilterSPTheoSize(Size s)
        {

            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;

            return View(context.GetProductsBaseSize(s.Id));




        }
        public IActionResult LocTheoBrand( Brand b)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;

            return View(context.GetProductBrand(b.Id));
        }
        public IActionResult TimSPByLQ( )
        {
            return View();
           
        }

        public IActionResult product_layout()
        {

            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            return View(context.GetProduct());
        }
        public IActionResult ShowProductNew()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            return View(context.GetProductNew());
        }

        public IActionResult ShowBestSeller()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            return View(context.GetProductBestSeller());
        }

        public IActionResult ShowPriceDESC()
        {

            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            return View(context.GetPriceDESC());
        }
        public IActionResult ShowPriceASC()
        {

            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            return View(context.GetPriceASC());
        }
        public IActionResult ShowProductPopular()
        {

            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            return View(context.GetProductPopular());
        }
        public IActionResult ShowProductWoman()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            return View(context.GetProductWoman());
        }

    }
}
