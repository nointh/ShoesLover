using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesLover.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesLover.Areas.Admin.Controllers
{       
    [Area("Admin")]        
    [Authorize]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
            return View(context.GetUsers());

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
                if (context.DeleteUser(id) > 0)
                {
                    TempData["message"] = "Xóa người dùng thành công";
                }
                else
                {
                    TempData["message"] = "Xóa người dùng thất bại";
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
