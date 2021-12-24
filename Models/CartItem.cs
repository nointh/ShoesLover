using ShoesLover.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesLover.Models
{
    public class CartItem
    {
        public int UserId { get; set; }
        public int ProductDetailId { get; set; }
        public int Quantity { get; set; }
    }
    public class CartItemDetail : CartItem
    {
        public string Image { get; set; }
        public string ProductName { get; set; }
        public string SizeName { get; set; }
        public string ColorName { get; set; }
        public int PricePerUnit { get; set; }
        public static CartItemDetail ParseCartItem(StoreContext store, CartItem item)
        {
            CartItemDetail detail = new CartItemDetail();

            detail.ProductDetailId = item.ProductDetailId;
            detail.UserId = item.UserId;
            detail.Quantity = item.Quantity;

            ProductDetail productDetail = store.GetProductDetail(item.ProductDetailId);
            Product product =  store.GetProductById(productDetail.ProductId);
            detail.PricePerUnit = Convert.ToInt32(product.RegularPrice);
            detail.ProductName = product.ProductName;
            detail.Image = product.DefaultImage;
            detail.ColorName = store.GetColorById(productDetail.ColorId).ColorName;
            detail.SizeName = store.GetSizeById(productDetail.SizeId).SizeName;

            return detail;
        }
    }
}
