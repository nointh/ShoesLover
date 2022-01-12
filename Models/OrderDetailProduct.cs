using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesLover.Models
{
    public class OrderDetailProduct
    {
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string ColorImage { get; set; }
        public string BrandName { get; set; }
        public string SizeName { get; set; }
        public int Quantity { get; set; }
        public double SalePrice { get; set; }
        public int ColorID { get; set; }
        public int ProductID { get; set; }
        public int OrderID { get; set; }
    }
}
