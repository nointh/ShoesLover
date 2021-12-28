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
        public CartItemDetail ParseCartDetailItem(StoreContext store)
        {
            CartItemDetail detail = new CartItemDetail();

            detail.ProductDetailId = this.ProductDetailId;
            detail.UserId = this.UserId;
            detail.Quantity = this.Quantity;

            ProductDetail productDetail = store.GetProductDetail(this.ProductDetailId);
            Product product = store.GetProductById(productDetail.ProductId);
            detail.PricePerUnit = Convert.ToInt32(product.SalePrice);
            detail.ProductName = product.ProductName;
            detail.Image = product.DefaultImage;
            detail.ColorName = store.GetColorById(productDetail.ColorId).ColorName;
            detail.SizeName = store.GetSizeById(productDetail.SizeId).SizeName;

            return detail;
        }
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
            detail.PricePerUnit = Convert.ToInt32(product.SalePrice);
            detail.ProductName = product.ProductName;
            detail.Image = product.DefaultImage;
            detail.ColorName = store.GetColorById(productDetail.ColorId).ColorName;
            detail.SizeName = store.GetSizeById(productDetail.SizeId).SizeName;

            return detail;
        }
        public static void AddToCartList(List<CartItemDetail> cartList, CartItemDetail newItem)
        {
            var x = cartList.Where(item => item.ProductDetailId == newItem.ProductDetailId).ToList();
            if (x.Count == 0)
            {
                cartList.Add(newItem);
            }
            else
            {
                foreach (var _product in cartList)
                {
                    if (_product.ProductDetailId == newItem.ProductDetailId)
                    {
                        _product.Quantity += newItem.Quantity;
                    }
                }
            }
        }
    }
}
