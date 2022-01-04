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
        public string ImageBig1 { get; set; }
        public string ImageBig2 { get; set; }
        public string ImageBig3 { get; set; }
        public string ImageBig4 { get; set; }
        public string ImageBig5 { get; set; }
        public string ImageBig6 { get; set; }
        public bool Active { get; set; }
        public IFormFile ImageFile { get; set; }
        public IFormFile ImageFile1 { get; set; }
        public IFormFile ImageFile2 { get; set; }
        public IFormFile ImageFile3 { get; set; }
        public IFormFile ImageFile4 { get; set; }
        public IFormFile ImageFile5 { get; set; }
        public IFormFile ImageFile6 { get; set; }
    }
}
