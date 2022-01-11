using Microsoft.AspNetCore.Authorization;
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

    public class CommentController : Controller
    {
        public IActionResult Index()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
           
            return View(context.GetCommentAdmin());
        }

        public PartialViewResult ShowCommentParent(int comment_id)
        {

            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            ViewBag.showcomment = context.ShowCommentParent(comment_id);
            return PartialView();
        }

    }
}
