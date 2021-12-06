using Microsoft.AspNetCore.Mvc;
using ShoesLover.Data;
using ShoesLover.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shoeslover.Controllers
{
    public class brandController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult FilterBrand()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(ShoesLover.Data.StoreContext)) as StoreContext;

            return View(context.GetBrand());
        }
    }
}
