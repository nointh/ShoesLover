using Microsoft.AspNetCore.Mvc;
using ShoesLover.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesLover.Controllers
{
    public class CommentController : Controller
    {
        public IActionResult Index()
        {

            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;

            return View();
        }

        public PartialViewResult ShowComment(int product_id)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;        
            return PartialView(context.GetComment(product_id));
        }

        public PartialViewResult InsertComment(string comment_name, int product_id, string comment_text, int comment_status, int comment_color_id)
        {
            int count;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            count = context.InsertComment(comment_name, product_id, comment_text, comment_status, comment_color_id);
            if (count > 0)
            {
                ViewData["thongbao"] = "Thêm bình luận thành công, bình luận đang chờ duyệt!";
            }
            else
            {
                ViewData["thongbao"] = "Thêm bình luận thất bại!";
            }

            return PartialView();
        }
        public PartialViewResult UpdateComment(int product_id, string comment_id, int comment_status, int color_id)
        {
            int count;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            count = context.UpdateComment(product_id, comment_id, comment_status, color_id);
            if (count > 0)
            {
                ViewData["thongbao"] = "Update thành công";
            }
            else
            {
                ViewData["thongbao"] = "Update thất bại";
            }

            return PartialView();
        }

        public PartialViewResult ReplyComment(string comment_name, int product_id, string comment_text, int comment_status, int comment_color_id, int comment_parent_comment)
        {
            int count;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            count = context.ReplyComment(comment_name, product_id, comment_text, comment_status, comment_color_id, comment_parent_comment);
            if (count > 0)
            {
                ViewData["thongbao"] = "Trả lời bình luận thành công";
            }
            else
            {
                ViewData["thongbao"] = "Trả lời bình luận thất bại!";
            }

            return PartialView();
        }
       
    }
}
