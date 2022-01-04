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
            {
                ViewData["thongbao"] = "Đăng ký thành công";
            }
            else
                ViewData["thongbao"] = "Đăng ký không thành công";
            return View();
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
                TempData["message"] = "Đăng nhập thất bại";
                return RedirectToAction(nameof(DangNhap));
            }

            TempData["message"] = "Đăng nhập thành công";
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

