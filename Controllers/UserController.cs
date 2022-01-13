using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ShoesLover.Models;
using ShoesLover.Data;
using MySqlX.XDevAPI;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ShoesLover.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("user")))
            {
                return RedirectToAction(nameof(DangNhap));
            }
            User user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("user"));
            return View(user);
        }
        public IActionResult UpdateInfo(User user)
        {
            StoreContext db = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
            if (db.UpdateUserInfo(user) > 0)
            {
                TempData["message"] = "Update user info successfully";
                TempData["message-status"] = "success";
                User oldUser = db.GetUsers().Where(i => i.ID == user.ID).FirstOrDefault();
                oldUser.Fullname = user.Fullname;
                oldUser.Phone = user.Phone;
                HttpContext.Session.SetString("user", JsonConvert.SerializeObject(oldUser));
                return LocalRedirect("/");
            }
            else
            {
                TempData["message"] = "Update user info failed";
                TempData["message-status"] = "error";
                return RedirectToAction(nameof(Index));
            }
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
            {
                TempData["message"] = "Sign up successfully";
                TempData["message-status"] = "success";
                return RedirectToAction("index", "home");
            }
            else
            {
                TempData["message"] = "Sign up failed";
                TempData["message-status"] = "error";
                return RedirectToAction("DangKy");
            }
        }
        
        public IActionResult DangNhap(string? redirectOption)
        {
            if (redirectOption != null)
            {
                ViewData["redirectOption"] = redirectOption;
            }
            return View();
        }

        public IActionResult LogIn(string email, string password, string? redirectOption)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
            var user = context.LogIn(email, password);
            if (user == null)
            {
                TempData["message"] = "Log in failed";
                TempData["message-status"] = "error";
                return RedirectToAction(nameof(DangNhap));
            }

            TempData["message"] = "Log in successfully";
            TempData["message-status"] = "success";
            HttpContext.Session.SetString("user",JsonConvert.SerializeObject(user));
            List<CartItem> cartItems = context.GetCartItemList(user.ID);
            List<CartItemDetail> cartList = new List<CartItemDetail>();
            if (HttpContext.Session.GetString("cart") != null)
            {
                cartList = JsonConvert.DeserializeObject<List<CartItemDetail>>(HttpContext.Session.GetString("cart"));
            }
            foreach (var item in cartItems)
            {
                cartList.Add(item.ParseCartDetailItem(context));
            }
            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(cartList));
            if (redirectOption != null)
                return RedirectToAction("Checkout", "Order");
            return RedirectToAction("Index", "Home");
        }


        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("user");
            HttpContext.Session.Remove("cart");
            HttpContext.Session.Remove("checkout");
            return RedirectToAction("Index", "Home");
        }
    }
}

