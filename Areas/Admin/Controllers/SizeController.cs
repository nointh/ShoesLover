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
    public class SizeController : Controller
    {
        // GET: SizeController
        public ActionResult Index()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
            return View(context.GetSizes());
        }

        // GET: SizeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SizeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SizeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Size Size)
        {
            try
            {
                StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
                if (context.InsertSize(Size) > 0)
                {
                    TempData["message"] = "Thêm mới kích thước thành công";
                }
                else
                {
                    TempData["message"] = "Thêm mới kích thước thất bại";
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

        // GET: SizeController/Edit/5
        public ActionResult Edit(int id)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
            return View(context.GetSizeById(id));
        }

        // POST: SizeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Size Size)
        {
            try
            {
                StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
                if (context.UpdateSize(Size) > 0 )
                {
                    TempData["message"] = "Cập nhật kích thước thành công";
                }
                else
                {
                    TempData["message"] = "Cập nhật kích thước thất bại";
                    return View(Size);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["message"] = "Có lỗi xảy ra";
                return View(Size);
            }
        }

        // GET: SizeController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: SizeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
                if (context.DeleteSize(id) > 0)
                {
                    TempData["message"] = "Xóa kích thước thành công";
                }
                else
                {
                    TempData["message"] = "Xóa kích thước thất bại";
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
