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
    public class BrandController : Controller
    {
        // GET: BrandController
        public ActionResult Index()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
            return View(context.GetBrands());
        }

        // GET: BrandController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BrandController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BrandController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Brand brand)
        {
            try
            {
                StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
                if (context.InsertBrand(brand) > 0)
                {
                    TempData["message"] = "Thêm mới nhãn hiệu thành công";
                }
                else
                {
                    TempData["message"] = "Thêm mới nhãn hiệu thất bại";
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["message"] = "Có lỗi xảy ra";
                return View();
            }
        }

        // GET: BrandController/Edit/5
        public ActionResult Edit(int id)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
            return View(context.GetBrandById(id));
        }

        // POST: BrandController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Brand brand)
        {
            try
            {
                StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
                if (context.UpdateBrand(brand) > 0 )
                {
                    TempData["message"] = "Thêm mới nhãn hiệu thành công";
                }
                else
                {
                    TempData["message"] = "Thêm mới nhãn hiệu thất bại";
                    return View(brand);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["message"] = "Có lỗi xảy ra";
                return View(brand);
            }
        }

        // GET: BrandController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: BrandController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
                if (context.DeleteBrand(id) > 0)
                {
                    TempData["message"] = "Xóa nhãn hiệu thành công";
                }
                else
                {
                    TempData["message"] = "Xóa nhãn hiệu thất bại";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["mesage"] = "Có lỗi xảy ra!!";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
