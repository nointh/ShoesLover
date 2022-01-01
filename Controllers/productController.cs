using Microsoft.AspNetCore.Mvc;
using ShoesLover.Data;
using ShoesLover.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ShoesLover.Controllers
{
    public class productController : Controller
    {
        public IActionResult Index(int? page)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            ViewBag.pagination_woman = context.GetProductWoman();
            int pagesize = 6;
            int pagenumber = (page ?? 1);

            return View(ViewBag.pagination_woman(pagenumber, pagesize));
        }



        public IActionResult TimSanPham()
        {
            return View();

        }
        public IActionResult SearchProductByName(int page, string keyword)
        {
            int count;
            int start = page * 6 - 6;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            ViewBag.ShowAllProductsSearch = context.GetAllProductsSearch(keyword);
            return View(context.GetProductsSearch(start, keyword));
            if (count > 0)
                ViewData["thongbao"] = "Tìm thấy";
            else
                ViewData["thongbao"] = "Tìm không thấy";
            return View();
        }





        public IActionResult ShowProductNewCate(int page, int cate_id)
        {
            int start = page * 6 - 6;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            ViewBag.ShowNew = context.GetProductNewCate(start, cate_id);
            ViewBag.ShowAllProductsCate = context.GetAllProductsNewCate(cate_id);
            return View(context.GetCategoryById(cate_id));
        }


        public IActionResult ShowProductNewSubCate(int page, int subcate_id)
        {
            int start = page * 6 - 6;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            ViewBag.ShowNew = context.GetProductNewSub(start, subcate_id);
            ViewBag.ShowAllProductsSub = context.GetAllProductsNewSub(subcate_id);
            return View(context.GetSubCate(subcate_id));
        }


        public IActionResult ShowBestSellerCate(int page, int cate_id)
        {
            int start = page * 6 - 6;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            ViewBag.ShowBestSeller = context.GetProductBestSellerCate(start, cate_id);
            ViewBag.ShowAllProductsCate = context.GetAllProductsBestSellerCate(cate_id);
            return View(context.GetCategoryById(cate_id));
        }
        public IActionResult ShowBestSellerSubCate(int page, int subcate_id)
        {
            int start = page * 6 - 6;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            ViewBag.ShowBestSeller = context.GetProductBestSellerSub(start, subcate_id);
            ViewBag.ShowAllProductsSub = context.GetAllProductsBestSellerSub(subcate_id);
            return View(context.GetSubCate(subcate_id));
        }

        public IActionResult ShowPriceDESCCate(int page, int cate_id)
        {
            int start = page * 6 - 6;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            ViewBag.ShowDESC = context.GetProductDESCCate(start, cate_id);
            ViewBag.ShowAllProductsCate = context.GetAllProductDESCCate(cate_id);
            return View(context.GetCategoryById(cate_id));
        }
        public IActionResult ShowPriceDESCSubCate(int page, int subcate_id)
        {

            int start = page * 6 - 6;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            ViewBag.ShowDESC = context.GetProductDESCSub(start, subcate_id);
            ViewBag.ShowAllProductsSub = context.GetAllProductsDESCSub(subcate_id);
            return View(context.GetSubCate(subcate_id));
        }
        public IActionResult ShowPriceASCCate(int page, int cate_id)
        {
            int start = page * 6 - 6;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            ViewBag.ShowASC = context.GetProductASCCate(start, cate_id);
            ViewBag.ShowAllProductsCate = context.GetAllProductsASCCate(cate_id);
            return View(context.GetCategoryById(cate_id));
        }
        public IActionResult ShowPriceASCSubCate(int page, int subcate_id)
        {

            int start = page * 6 - 6;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            ViewBag.ShowASC = context.GetProductASCSub(start, subcate_id);
            ViewBag.ShowAllProductsSub = context.GetAllProductsASCSub(subcate_id);
            return View(context.GetSubCate(subcate_id));
        }
        public IActionResult ShowProductPopularCate(int page, int cate_id)
        {
            int start = page * 6 - 6;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            ViewBag.ShowPopular = context.GetProductPopularCate(start, cate_id);
            ViewBag.ShowAllProductsCate = context.GetAllProductsPopularCate(cate_id);
            return View(context.GetCategoryById(cate_id));
        }
        public IActionResult ShowProductPopularSubCate(int page, int subcate_id)
        {
            int start = page * 6 - 6;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            ViewBag.ShowPopular = context.GetProductPopularSubCate(start, subcate_id);
            ViewBag.ShowAllProductsSub = context.GetAllProductsPopularSub(subcate_id);
            return View(context.GetSubCate(subcate_id));
        }


        public IActionResult ShowProductDetailObject(int color_id, int product_id)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            ViewBag.GetSizeByID = context.GetSizeByIDPro(color_id, product_id);
            ViewBag.SPlienquan = context.GetProductsRelateBaseSub(product_id);
            ViewBag.ShowColorProduct = context.GetColorsOfProduct(product_id);
            return View(context.GetProductObject(color_id, product_id));
        }

        public IActionResult ShowProducts(int page, int subcate_id)
        {
            int start = page * 6 - 6;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            ViewBag.ShowProducts = context.GetProductsBySubcategoryID(start, subcate_id);
            ViewBag.ShowAllProductsSub = context.GetAllProductsSub(subcate_id);
            return View(context.GetSubCate(subcate_id));
        }
        public IActionResult ShowProductsCate(int page, int cate_id)
        {
            int start = page * 6 - 6;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            ViewBag.ShowProductsCate = context.GetProductsCateObj(start, cate_id);
            ViewBag.ShowAllProductsCate = context.GetAllProductsCate(cate_id);
            return View(context.GetCategoryById(cate_id));
        }

        public JsonResult GetProductVariantImage(int product_id, int color_id)
        {
            IEnumerable<ProductVariantDefault> pro;
            try
            {
                StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
                pro = context.GetVariantImg(product_id, color_id);
            }
            catch
            {
                return Json(new { err = "error" });
            }
            return Json(pro);
        }
        public JsonResult GetProductProductDetailId([FromBody] string jsonData)
        {
            try
            {
                dynamic data = JsonConvert.DeserializeObject(jsonData);
                int colorId = Convert.ToInt32(data["colorId"]);
                int productId = Convert.ToInt32(data["productId"]);
                int sizeId = Convert.ToInt32(data["sizeId"]);

                StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
                int detailId = context.GetProductDetailId(productId, colorId, sizeId);
                return Json(new { id = detailId });
            }
            catch
            {
                return Json(new { err = "error" });
            }
        }
    }
}
