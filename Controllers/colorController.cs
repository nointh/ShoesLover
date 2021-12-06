using Microsoft.AspNetCore.Mvc;
using ShoesLover.Data;
using ShoesLover.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesLover.Controllers
{
    public class colorController:Controller
    {
        public IActionResult LietKeColor()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;
            return View(context.GetColors());
        }
        public IActionResult FilterSPTheoCoLor()
        {

            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;

            return View(context.GetColors());

        }
    }
    
}
