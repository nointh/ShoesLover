using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesLover.Models
{
    public class OrderDetail
    {
        public int OrderId { get; set; }
        public int ProductDetailId { get; set; }
        public int Quantity { get; set; }
    }
}
