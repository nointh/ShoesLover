using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesLover.Models
{
    public class ProductMasterData:Product
    {
        public ProductMasterData(Product product)
        {
            this.Id = product.Id;
            this.ProductName = product.ProductName;
            this.Gender = product.Gender;
            this.BrandId = product.BrandId;
            this.SubCategoryId = product.SubCategoryId;
            this.DefaultImage = product.DefaultImage;
            this.Description = product.Description;
            this.Active = product.Active;
            ProductVariants = new List<ProductVariantDetail>();
        }
        public List<ProductVariantDetail> ProductVariants;
    }
    public class ProductVariantDetail : ProductColorVariant
    {
        public ProductVariantDetail(ProductColorVariant variant)
        {
            this.ColorId = variant.ColorId;
            this.ProductId = variant.ProductId;
            this.ProductVariantImage = variant.ProductVariantImage;
            this.Active = variant.Active;
            ProductDetails = new List<ProductDetail>();
        }
        public List<ProductDetail> ProductDetails;
    }
}
