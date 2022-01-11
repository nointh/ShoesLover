using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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

    public class ColorController : Controller
    {
        // GET: ColorController
        public ActionResult Index()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
            return View(context.GetColors());
        }

        // GET: ColorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ColorController/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: ColorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create( Color color)
        {
            try
            {
                StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
                int result = await context.InsertColorAsync(color);
                if (result > 0)
                {
                    TempData["message"] = "Thêm mới màu sắc thành công";
                }
                else
                {
                    TempData["message"] = "Thêm mới màu sắc thất bại";
                    return View();
                }    
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                TempData["message"] = "Có lỗi xảy ra!!" + e.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: ColorController/Edit/5
        public ActionResult Edit(int id)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;

            return View(context.GetColorById(id));
        }

        // POST: ColorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Color color)
        {
            try
            {
                StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
                if (await context.UpdateColorAsync(color) > 0)
                {
                    TempData["message"] = "Cập nhật màu sắc thành công";
                }    
                else
                {
                    TempData["message"] = "Cập nhật màu sắc không thành công";
                    return View(color);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["message"] = "Có lỗi xảy ra!!";
                return View(color);
            }
        }

        // GET: ColorController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: ColorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
                if (context.DeleteColor(id) > 0)
                {
                    TempData["message"] = "Xóa màu sắc thành công";
                }
                else
                {
                    TempData["message"] = "Xóa màu sắc không thành công";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["message"] = "Có lỗi xảy ra!!";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
