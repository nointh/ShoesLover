using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesLover.Data;
using ShoesLover.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ShoesLover.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SetCheckoutList([FromBody]string productList)
        {
            HttpContext.Session.SetString("checkout", productList);
            return Json(new { success = true });
        }
        [HttpPost]
        public IActionResult SetCheckoutListDirectly(int colorId,int sizeId, int productId, int quantity)
        {
            StoreContext store = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
            int productDetailId = store.GetProductDetailId(productId, colorId, sizeId);
            CartItem checkout = new CartItem
            {
                ProductDetailId = productDetailId,
                UserId = 0,
                Quantity = quantity
            };
            List<CartItem> list = new List<CartItem>();
            list.Add(checkout);
            HttpContext.Session.SetString("checkout", JsonConvert.SerializeObject(list));
            return Json(new { success = true });
        }
        public IActionResult Checkout()
        {
            if (HttpContext.Session.GetString("checkout") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (HttpContext.Session.GetString("user") == null )
            {
                return RedirectToAction("DangNhap", "User", new { redirectOption = "checkout" });
            }    
            return View();
        }
        public IActionResult ConfirmOrder(string fullname, string phone, string fullAddress)
        {

            StoreContext store = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
            if (HttpContext.Session.GetString("user") == null || HttpContext.Session.GetString("checkout") == null)
            {
                return RedirectToAction("DangNhap", "User");
            }
            User user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("user"));
            List<CartItem> checkoutList = JsonConvert.DeserializeObject<List<CartItem>>(HttpContext.Session.GetString("checkout"));
            Order order = new Order
            {
                UID = user.ID,
                Name = fullname,
                Address = fullAddress,
                Phone = phone,
                OrderDate = DateTime.Now
            };
            if (store.CreateOrder(order, checkoutList) > 0)
            {
                if (HttpContext.Session.GetString("cart") != null)
                {
                    List<int> removeIDs = new List<int>();
                    List<CartItemDetail> cartList = JsonConvert.DeserializeObject<List<CartItemDetail>>(HttpContext.Session.GetString("cart"));
                    foreach(var item in cartList)
                    {
                        if(checkoutList.Where(i => i.ProductDetailId == item.ProductDetailId).Count() > 0)
                        {
                            CartItem checkoutItem = checkoutList.Where(i => i.ProductDetailId == item.ProductDetailId).FirstOrDefault();
                            if (checkoutItem.Quantity >= item.Quantity)
                            {
                                removeIDs.Add(item.ProductDetailId);
                            }
                            else item.Quantity -= checkoutItem.Quantity;
                        }
                    }
                    cartList = cartList.Where(i => !removeIDs.Contains(i.ProductDetailId)).ToList();
                    HttpContext.Session.SetString("cart",JsonConvert.SerializeObject(cartList));
                    HttpContext.Session.Remove("checkout");
                    store.UpdateCartList(cartList, user.ID);
                }
                TempData["message"] = "Order created";
                TempData["message-status"] = "success";
                return RedirectToAction("index", "home");
            }
            return RedirectToAction("checkout");
        }
    }
}
