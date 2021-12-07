using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ShoesLover.Models;
using ShoesLover.Data;


namespace ShoesLover.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DangKy()
        {
            return View();
        }
        public IActionResult InsertIn4(User usr)
        {
            int count;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
            count = context.InsertIn4(usr);
            ViewData.Model = usr;
            if (count > 0)
                ViewData["thongbao"] = "Sign in thành công";
            else
                ViewData["thongbao"] = "Sign in không thành công";
            return View();
        }
        
        public IActionResult DangNhap()
        {
            return View();
        }


        public IActionResult LogIn(string fullname, string password)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;

            return View(context.LogIn(fullname, password));
        }


        public IActionResult LogOut()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}

