using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesLover.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ShoesLover.Models;

namespace ShoesLover.Controllers
{
    public class CartController : Controller
    {        // GET: CartController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CartController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CartController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CartController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CartController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CartController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CartController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CartController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public IActionResult ProductView()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddToCart(int productId, int colorId, int sizeId, int quantity)
        {
            StoreContext store = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;

            CartItem item = new CartItem
            {
                ProductDetailId = store.GetProductDetailId(productId, colorId, sizeId),
                Quantity = quantity
            };
            CartItemDetail currentCartItem = CartItemDetail.ParseCartItem(store, item);
            List<CartItemDetail> listCart;
            if (HttpContext.Session.GetString("cart") == null)
            {
                listCart = new List<CartItemDetail>();
                listCart.Add(currentCartItem);
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(listCart));
            }
            else
            {
                listCart = JsonConvert.DeserializeObject<List<CartItemDetail>>(HttpContext.Session.GetString("cart"));
                CartItemDetail.AddToCartList(listCart, currentCartItem);
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(listCart));
            }
            return Json(currentCartItem);
        }
    }
}
