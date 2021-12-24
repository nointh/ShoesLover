using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesLover.Models
{
    public class ProductObject
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public int Quantity { get; set; }
        public bool Active { get; set; }
        public string DefaultImage { get; set; }
        public string Product_detail_img_1 { get; set; }
        public string Product_detail_img_2 { get; set; }
        public string Product_detail_img_3 { get; set; }
        public string Product_detail_img_4 { get; set; }
        public string Product_detail_img_5 { get; set; }
        public string Product_detail_img_6 { get; set; }
        public string Product_detail_big_img_1 { get; set; }
        public string Product_detail_big_img_2 { get; set; }
        public string Product_detail_big_img_3 { get; set; }
        public string Product_detail_big_img_4 { get; set; }
        public string Product_detail_big_img_5 { get; set; }
        public string Product_detail_big_img_6 { get; set; }
        public string ColorName { get; set; }
        public string ColorImage { get; set; }

        public string ProductName { get; set; }
        public string ProductTag { get; set; }
        public string SizeName { get; set; }
        public string CategoryName { get; set; }
        public string SubcategoryName { get; set; }
        public int Category_id { get; set; }
        public int Subcategory_id { get; set; }
        public int Gender { get; set; }

        public long SalePrice { get; set; }
        public long RegularPrice { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }

    }
}
