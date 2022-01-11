using Microsoft.AspNetCore.Mvc;
using ShoesLover.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesLover.Controllers
{
    public class RatingController : Controller
    {
        public IActionResult InsertRating(int product_id, int index, int color_id)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;


            int count;
            count = context.InsertRating(product_id, index, color_id);
            if (count > 0)
            {
                ViewData["thongbao"] = "Đánh giá thành công";
            }
            else
            {
                ViewData["thongbao"] = "Đánh giá thất bại!";
            }          
            return View();
        }
    }
}
