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

    public class CategoryController : Controller
    {
        // GET: CategoryController
        public ActionResult Index()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
            return View(context.GetCategoryMasters());
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
            return View(context.GetCategoryMasterDataById(id));
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category, string redirectOption)
        {
            try
            {
                StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
                Dictionary<string, int> resultDict = context.InsertCategory(category);
                if (resultDict.ContainsKey("result") && resultDict["result"] >0)
                {
                    TempData["message"] = "Thêm danh mục mới thành công";
                    if (redirectOption != null && redirectOption.Equals("continue"))
                    {
                        int _id = resultDict.ContainsKey("id") ? resultDict["id"] : -1;
                        return RedirectToAction(nameof(Details), new { id = _id });
                    }
                }
                else
                {
                    TempData["message"] = "Thêm danh mục mới thất bại";
                    return View();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["message"] = "Có lỗi xảy ra!!";
                return View();
            }
        }
        // GET: CategoryController/CreateSubCategory
        public ActionResult CreateSubCategory(int id)
        {
            return View(id);
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSubCategory(SubCategory subCategory)
        {
            try
            {
                StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
                TempData["message"] = 
                    (context.InsertSubCategory(subCategory) > 0) 
                    ? "Thêm danh mục con thành công" 
                    : "Thêm danh mục con thất bại";
  
                //return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["message"] = "Có lỗi xảy ra";
            }                
            return RedirectToAction(nameof(Details), new { id = subCategory.Id });

        }
        // GET: CategoryController/EditSubCategory/5
        public ActionResult EditSubCategory(int id)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;     
            return View(context.GetSubCategoryById(id));
        }

        // POST: CategoryController/EditSubCategory
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSubCategory(SubCategory subCategory)
        {
            try
            {
                StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
                TempData["message"] =
                    (context.UpdateSubCategory(subCategory) > 0)
                    ? "Cập nhật danh mục con thành công"
                    : "Cập nhật danh mục con thất bại";

                //return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["message"] = "Có lỗi xảy ra";
            }
            return RedirectToAction(nameof(Details), new { id = subCategory.CategoryId });

        }
        // POST: CategoryController/EditSubCategory
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSubCategory(int id)
        {
            int categoryId = 0;
            try
            {
                StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
                categoryId = context.GetSubCategoryById(id).CategoryId;

                TempData["message"] =
                    (context.DeleteSubCategory(id) > 0)
                    ? "Xóa danh mục con thành công"
                    : "Xóa danh mục con thất bại";

                //return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["message"] = "Có lỗi xảy ra";
            }
            return RedirectToAction(nameof(Details), new { id = categoryId });

        }
        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;

            return View(context.GetCategoryById(id));
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            try
            {
                StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
                TempData["message"] =
                    (context.UpdateCategory(category) > 0)
                    ? "Cập nhật danh mục thành công"
                    : "Cập nhật danh mục thất bại";

                //return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["message"] = "Có lỗi xảy ra";
            }
            return RedirectToAction(nameof(Details), new { id = category.Id });
        }

        // GET: CategoryController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
                TempData["message"] =
                    (context.DeleteCategory(id) > 0)
                    ? "Xóa danh mục thành công"
                    : "Xóa danh mục thất bại";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["message"] = "Có lỗi xảy ra!!";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public JsonResult GetSubCategoryList(int id)
        {
            IEnumerable<SubCategory> subCategories;
            try
            {
                StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
                subCategories = context.GetSubCategoriesByCateId(id);
            }
            catch
            {
                return Json(new { err = "error" });
            }
            return Json(subCategories);
        }
    }
}
