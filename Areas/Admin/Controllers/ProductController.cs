using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [Authorize]

    public class ProductController : Controller
    {

        public IActionResult Index()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
            ViewData["context"] = context;
            return View(context.GetProducts().Where<Product>(item => item.Active));
        }
        public IActionResult CreateProduct()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
            IEnumerable<CategoryMasterModel> allCategory = context.GetCategoryMasters();
            ViewData["category"] = allCategory.Where(item => item.SubCategoryList.Count > 0);
            ViewData["brand"] = context.GetBrands();

            return View();
        }
        public IActionResult EditProduct(int id)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
            IEnumerable<CategoryMasterModel> allCategory = context.GetCategoryMasters();
            ViewData["category"] = allCategory.Where(item => item.SubCategoryList.Count > 0);
            ViewData["brand"] = context.GetBrands();
            Product product = context.GetProductById(id);
            ViewBag.CurrentSubcategory = context.GetSubCategoryById(product.SubCategoryId);
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(Product product, string? redirectOption)
        {            
            ViewData.Model = product;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
            int[] result = await context.InsertProduct(product);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(Product product)
        {
            //ViewData.Model = product;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
            int result = await context.UpdateProduct(product);
            TempData["message"] = result > 0 ? "Cập nhật sản phẩm thành công" : "Cập nhật sản phẩm thất bại";
            return RedirectToAction("ProductDetails", new { id = product.Id});
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
                int result = context.DeleteProduct(id);
                TempData["message"] = result > 0 ? "Xóa sản phẩm thành công" : "Xóa sản phẩm thất bại";
            }
            catch
            {
                TempData["message"] = "Có lỗi xảy ra";
            }

            return RedirectToAction("Index");
        }
        // GET: ProductController/ProductDetails/5
        public ActionResult ProductDetails(int id)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
            ViewBag.Colors = context.GetColors();
            List<Color> availableColors = new List<Color>();
            var productMasterData = context.GetProductMasterData(id);
            foreach (var variant in productMasterData.ProductVariants)
            {
                availableColors.Add(context.GetColorById(variant.ColorId));
            }
            ViewBag.AvailableColors = availableColors;
            ViewBag.Sizes = context.GetSizes();
            ViewBag.BrandName = context.GetBrandById(productMasterData.BrandId).BrandName;
            ViewBag.SubcategoryName = context.GetSubCategoryById(productMasterData.SubCategoryId).SubCategoryName;
            int gender = productMasterData.Gender;
            if (gender == 0)
                ViewBag.GenderString = "Nữ";
            else if (gender == 1)
                ViewBag.GenderString = "Nam";
            else ViewBag.GenderString = "Cả hai";
            ViewBag.BrandName = context.GetBrandById(productMasterData.BrandId).BrandName;

            return View(productMasterData);
        }
        public IActionResult SetupVariant(int id)
        {
            return View();
        }
        public IActionResult CreateVariant(int id)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
            ViewBag.ProductId = id;
            ViewBag.Colors = context.GetColors().Where<Color>(color => color.Active == true);
            return View(id);
        }
        public IActionResult EditVariant(int productId, int colorId)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
            ViewBag.ColorName = context.GetColors().Where<Color>(color => color.Id == colorId).FirstOrDefault().ColorName;
            return View(context.GetProductVariantById(productId, colorId));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVariant(ProductColorVariant variant)
        {
            try
            {
                StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
                if (await context.InsertColorVariant(variant) > 0)
                {
                    TempData["message"] = "Thêm mới biến thể sản phẩm thành công";
                }
                else
                {
                    TempData["message"] = "Thêm mới biến thể sản phẩm thất bại";
                    return View();
                }
                return RedirectToAction(nameof(ProductDetails), new { id = variant.ProductId});
            }
            catch
            {
                TempData["message"] = "Có lỗi xảy ra";
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditVariant(ProductColorVariant variant)
        {
            try
            {
                StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
                if (await context.UpdateProductVariant(variant) > 0)
                {
                    TempData["message"] = "Cập nhật biến thể sản phẩm thành công";
                }
                else
                {
                    TempData["message"] = "Cập nhật biến thể sản phẩm thất bại";
                }
                return RedirectToAction(nameof(ProductDetails), new { id = variant.ProductId });
            }
            catch
            {
                TempData["message"] = "Có lỗi xảy ra";
                return RedirectToAction(nameof(ProductDetails), new { id = variant.ProductId });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteVariant(int colorId, int productId)
        {
            try
            {
                StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
                if (context.DeleteProductVariant(productId, colorId) > 0)
                {
                    TempData["message"] = "Xóa biến thể sản phẩm thành công";
                }
                else
                {
                    TempData["message"] = "Xóa biến thể sản phẩm thất bại";
                    return View();
                }
                return RedirectToAction(nameof(ProductDetails), new { id = productId });
            }
            catch
            {
                TempData["message"] = "Có lỗi xảy ra";
                return RedirectToAction(nameof(ProductDetails), new { id = productId });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateProductDetail(ProductDetail productDetail)
        {
            try
            {
                StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
                if (context.InsertProductDetail(productDetail) > 0)
                {
                    TempData["message"] = "Thêm mới chi tiêt sản phẩm thành công";
                }
                else
                {
                    TempData["message"] = "Thêm mới chi tiêt sản phẩm thất bại";
                }
                return RedirectToAction(nameof(ProductDetails), new { id = productDetail.ProductId });
            }
            catch
            {
                TempData["message"] = "Có lỗi xảy ra";
                return RedirectToAction(nameof(ProductDetails), new { id = productDetail.ProductId });
            }
        }
      /* [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateProductVariant(ProductColorVariant variant)
        {
          
                StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
                if (context.InsertProductDetail(variant) > 0)
                {
                    TempData["message"] = "Thêm mới chi tiêt sản phẩm thành công";
                }
                else
                {
                    TempData["message"] = "Thêm mới chi tiêt sản phẩm thất bại";
                }
                return RedirectToAction(nameof(ProductDetails), new { id = variant.ProductId });
            }
            catch
            {
                TempData["message"] = "Có lỗi xảy ra";
                return RedirectToAction(nameof(ProductDetails), new { id = variant.ProductId });
            }
        }  */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProductDetail(ProductDetail productDetail)
        {
            try
            {
                StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
                if (context.UpdateProductDetail(productDetail) > 0)
                {
                    TempData["message"] = "Cập nhật chi tiêt sản phẩm thành công";
                }
                else
                {
                    TempData["message"] = "Cập nhật chi tiêt sản phẩm thất bại";
                }
                return RedirectToAction(nameof(ProductDetails), new { id = productDetail.ProductId });
            }
            catch
            {
                TempData["message"] = "Có lỗi xảy ra";
                return RedirectToAction(nameof(ProductDetails), new { id = productDetail.ProductId });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProductDetail(ProductDetail productDetail)
        {
            try
            {
                StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
                if (context.DeleteProductDetail(productDetail.Id) > 0)
                {
                    TempData["message"] = "Xóa chi tiêt sản phẩm thành công";
                }
                else
                {
                    TempData["message"] = "Xóa chi tiêt sản phẩm thất bại";
                }
                return RedirectToAction(nameof(ProductDetails), new { id = productDetail.ProductId });
            }
            catch
            {
                TempData["message"] = "Có lỗi xảy ra";
                return RedirectToAction(nameof(ProductDetails), new { id = productDetail.ProductId });
            }
        }
    }
}
