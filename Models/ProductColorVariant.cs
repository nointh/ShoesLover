using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesLover.Models
{
    public class ProductColorVariant
    {
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public string ProductVariantImage { get; set; }
        public bool Active { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
