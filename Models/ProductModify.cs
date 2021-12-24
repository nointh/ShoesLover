using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesLover.Models
{
    public class ProductModify
    {
        public int Id { get; set; }
        [DisplayName("Tên sản phẩm")]
        public string ProductName { get; set; }
        public int SubCategoryId { get; set; }
        public int BrandId { get; set; }
        public int Gender { get; set; }
        public string DefaultImage { get; set; }
        public string DefaultImage_2 { get; set; }
        public string DefaultImage_3 { get; set; }
        public string DefaultImage_4 { get; set; }
        public double RegularPrice { get; set; }
        public double SalePrice { get; set; }
        public bool Active { get; set; }
        public string Description { get; set; }
        // public IFormFile ImageFile { get; set; }
        public bool ProductNew { get; set; }
        public string Product_detail_img_1 { get; set; }
        public string Product_detail_img_2 { get; set; }
        public string Product_detail_img_3 { get; set; }
        public string Product_detail_img_4 { get; set; }
        public string ProductTag { get; set; }
    }
}
