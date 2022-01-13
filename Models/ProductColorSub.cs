using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesLover.Models
{
    public class ProductColorSub : Product
    {
        public List<Color> productcolorMaster;
        public List<ProductColorVariant> productvariant;
        public double ratingProduct;
    }
}
