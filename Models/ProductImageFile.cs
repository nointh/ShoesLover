using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesLover.Models
{
    public class ProductImageFile
    {
        public string ImageName { get; set; }
        public IFormFile ImageFile { get; set; }
        public ProductImageFile (string str, IFormFile f)
        {
            this.ImageName = str;
            this.ImageFile = f;
        }
    }
}
