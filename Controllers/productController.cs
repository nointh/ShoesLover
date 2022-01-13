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
            int start = page * 9 - 9;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            ViewBag.ShowAllProductsSearch = context.GetAllProductsSearch(keyword);
            return View(context.GetProductsSearch(start, keyword));
            if (count > 0)
                ViewData["thongbao"] = "Tìm thấy";
            else
                ViewData["thongbao"] = "Tìm không thấy";
            return View();
        }





       


      /*  public IActionResult ShowBestSellerCate(int page, int cate_id)
        {
            int start = page * 9 - 9;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            ViewBag.ShowBestSeller = context.GetProductBestSellerCate(start, cate_id);
            ViewBag.ShowAllProductsCate = context.GetAllProductsBestSellerCate(cate_id);
            return View(context.GetCategoryById(cate_id));
        }
        public IActionResult ShowBestSellerSubCate(int page, int subcate_id)
        {
            int start = page * 9 - 9;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            ViewBag.ShowBestSeller = context.GetProductBestSellerSub(start, subcate_id);
            ViewBag.ShowAllProductsSub = context.GetAllProductsBestSellerSub(subcate_id);
            return View(context.GetSubCate(subcate_id));
        } */

        public PartialViewResult ShowPriceDESCCate(int page, int cate_id)
        {
            int start = page * 9 - 9;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            ViewBag.ShowProducts = context.GetProductDESCCate(start, cate_id);
            ViewBag.GetTotalPage = context.GetAllProductDESCCate(cate_id);
            ViewBag.GetCate = cate_id;
            return PartialView(context.GetCategoryById(cate_id));
        }
        public PartialViewResult ShowPriceDESCSubCate(int page, int subcate_id)
        {

            int start = page * 9 - 9;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            ViewBag.GetSubCate = subcate_id;
            ViewBag.ShowProducts = context.GetProductDESCSub(start, subcate_id);
            ViewBag.GetTotalPage = context.GetAllProductsDESCSub(subcate_id);
            return PartialView(context.GetSubCate(subcate_id));
        }
        public PartialViewResult ShowPriceASCCate(int page, int cate_id)
        {
            int start = page * 9 - 9;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            ViewBag.ShowProducts = context.GetProductASCCate(start, cate_id);
            ViewBag.GetTotalPage = context.GetAllProductsASCCate(cate_id);
            ViewBag.GetCate = cate_id;
            return PartialView(context.GetCategoryById(cate_id));
        }
        public PartialViewResult ShowPriceASCSubCate(int page, int subcate_id)
        {

            int start = page * 9 - 9;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            ViewBag.ShowProducts = context.GetProductASCSub(start, subcate_id);
            ViewBag.GetSubCate = subcate_id;
            ViewBag.GetTotalPage = context.GetAllProductsASCSub(subcate_id);
            return PartialView(context.GetSubCate(subcate_id));
        }      
        public IActionResult ShowProductDetailObject(int color_id, int product_id)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            ViewBag.GetSizeByID = context.GetSizeByIDPro(color_id, product_id);
            ViewBag.SPlienquan = context.GetProductsRelateBaseSub(product_id);
            ViewBag.ShowColorProduct = context.GetColorsOfProduct(product_id);
           
            
                ViewBag.ShowRating = context.GetRating(product_id, color_id);
            
          
            return View(context.GetProductObject(color_id, product_id));
        }

        public IActionResult ShowProducts(int page, int subcate_id)
        {
            int start = page * 9 - 9;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            ViewBag.ShowProducts = context.GetProductsBySubcategoryID(start, subcate_id);
            ViewBag.ShowAllProductsSub = context.GetAllProductsSub(subcate_id);
            ViewBag.ShowSizeSub = context.GetAllSizeSub(subcate_id);
            ViewBag.ShowColorSub = context.GetAllColorSub(subcate_id);
            
            return View(context.GetSubCate(subcate_id));
        }
        
        public IActionResult ShowProductsCate(int page, int cate_id)
        {
            int start = page * 9 - 9;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            ViewBag.ShowProductsCate = context.GetProductsCateObj(start, cate_id);
            ViewBag.ShowAllProductsCate = context.GetAllProductsCate(cate_id);
            ViewBag.ShowSizeCate = context.GetSizeCateID(cate_id);
            ViewBag.ShowColorCate = context.GetColorCateID(cate_id);
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
        public PartialViewResult GetProductBySizeSub(int page ,int size_id, int subcate_id)
        {
            int start = page * 9 - 9;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            ViewBag.GetSubCate = subcate_id;
            ViewBag.GetSize = size_id;
            ViewBag.GetTotalPage = context.GetAllProductSizeSub(size_id, subcate_id);
            ViewBag.ShowProducts = context.GetProductsBySizeSubID(start, size_id,subcate_id);           
            return PartialView();
        }
        public PartialViewResult GetProductByColorSub(int page, int color_id, int subcate_id)
        {
            int start = page * 9 - 9;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            ViewBag.GetSubCate = subcate_id;
            ViewBag.GetColor = color_id;
            ViewBag.GetTotalPage = context.GetAllProductColorSub(color_id, subcate_id);
            ViewBag.ShowProducts = context.GetProductsByColorSubID(start, color_id, subcate_id);
            return PartialView();
        }
        public PartialViewResult GetProductByColorSizeSub(int page, int color_id, int size_id, int subcate_id)
        {
            int start = page * 9 - 9;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            ViewBag.GetSubCate = subcate_id;
            ViewBag.GetSize = size_id;
            ViewBag.GetColor = color_id;
            ViewBag.GetTotalPage = context.GetAllProductColorSizeSub(color_id,size_id, subcate_id);
            ViewBag.ShowProducts = context.GetProductsByColorSizeSubID(start, color_id,size_id, subcate_id);
            return PartialView();
        }
        public PartialViewResult GetProductBySizeCate(int page, int size_id, int cate_id)
        {
            int start = page * 9 - 9;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            ViewBag.GetCate = cate_id;
            ViewBag.GetSize = size_id;
            ViewBag.GetTotalPage = context.GetAllProductSizeCate(size_id, cate_id);
            ViewBag.ShowProducts = context.GetProductsBySizeCateID(start, size_id, cate_id);
            return PartialView();
        }
        public PartialViewResult GetProductByColorCate(int page, int color_id, int cate_id)
        {
            int start = page * 9 - 9;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            ViewBag.GetCate = cate_id;
            ViewBag.GetColor = color_id;
            ViewBag.GetTotalPage = context.GetAllProductColorCate(color_id, cate_id);
            ViewBag.ShowProducts = context.GetProductsByColorCateID(start, color_id, cate_id);
            return PartialView();
        }
        public PartialViewResult GetProductByColorSizeCate(int page, int color_id, int size_id, int cate_id)
        {
            int start = page * 9 - 9;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            ViewBag.GetCate = cate_id;
            ViewBag.GetSize = size_id;
            ViewBag.GetColor = color_id;
            ViewBag.GetTotalPage = context.GetAllProductColorSizeCate(color_id, size_id, cate_id);
            ViewBag.ShowProducts = context.GetProductsByColorSizeCateID(start, color_id, size_id, cate_id);
            return PartialView();
        }

     
       /* public IActionResult ShowProductFilter(int page, int size_id, int color_id, int subcate_id)
        {
            string str = size_id.ToString();
            string stri = color_id.ToString();
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            int start = page * 9 - 9;
            ViewBag.ShowSizeSubFilter = context.GetAllSizeSubFilter(subcate_id);
            ViewBag.ShowColorSub = context.GetAllColorSub(subcate_id);
           if (String.IsNullOrEmpty(str) == false && String.IsNullOrEmpty(stri) == true)
           {
                //  ViewBag.GetSubCate = subcate_id;
                ViewBag.GetTotalPage = context.GetAllProductSizeSub(size_id, subcate_id);
                ViewBag.ShowProducts = context.GetProductsBySizeSubID(start, size_id, subcate_id);
                

            } 
            
            else if (String.IsNullOrEmpty(stri) == false && String.IsNullOrEmpty(str) == true)
            {
                ViewBag.GetTotalPage = context.GetAllProductColorSub(color_id, subcate_id);
                ViewBag.ShowProducts = context.GetProductsByColorSubID(start, color_id, subcate_id);
               
            }
            else
            {
                return View(context.GetSubCate(subcate_id));
            }
            return View(context.GetSubCate(subcate_id));
           

        }  */
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
