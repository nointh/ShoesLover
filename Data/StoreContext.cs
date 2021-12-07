using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using ShoesLover.Models;
using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace ShoesLover.Data
{
    public class StoreContext
    {
        private readonly string _connectionString;
        private readonly string _rootPath;
        public StoreContext(string connString, string rootPath)
        {
            _connectionString = connString;
            _rootPath = rootPath;
        }
        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
        public List<Product> GetProducts()
        {
            List<Product> listProduct = new List<Product>();
            try
            {
                using (var con = GetConnection())
                {   
                    con.Open();
                    string str = "select * from product";
                    MySqlCommand cmd = new MySqlCommand(str, con);
                    using (var reader = cmd.ExecuteReader())
                    {
                    
                        while (reader.Read())
                        {
                            listProduct.Add(new Product
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                ProductName = Convert.ToString(reader["productname"]),
                                SubCategoryId = Convert.ToInt32(reader["subcategory_id"]),
                                BrandId = Convert.ToInt32(reader["brand_id"]),
                                Gender = Convert.ToInt32(reader["gender"]),
                                DefaultImage = Convert.ToString(reader["default_image"]),
                                Description = Convert.ToString(reader["description"]),
                                SalePrice = Convert.ToDouble(reader["sale_price"]),
                                RegularPrice = Convert.ToDouble(reader["regular_price"]),
                                Active = Convert.ToBoolean(reader["active"])
                            });
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return listProduct;
        }
        public Product GetProductById(int id)
        {
            Product product = new Product();
            try
            {
                using (var con = GetConnection())
                {
                    con.Open();
                    string str = "select * from product where id = @id";
                    MySqlCommand cmd = new MySqlCommand(str, con);
                    cmd.Parameters.AddWithValue("id", id);
                    using (var reader = cmd.ExecuteReader())
                    {

                        reader.Read();
                        product.Id = Convert.ToInt32(reader["Id"]);
                        product.ProductName = Convert.ToString(reader["productname"]);
                        product.SubCategoryId = Convert.ToInt32(reader["subcategory_id"]);
                        product.BrandId = Convert.ToInt32(reader["brand_id"]);
                        product.Gender = Convert.ToInt32(reader["gender"]);
                        product.DefaultImage = Convert.ToString(reader["default_image"]);
                        product.Description = Convert.ToString(reader["description"]);
                        product.SalePrice = Convert.ToDouble(reader["sale_price"]);
                        product.RegularPrice = Convert.ToDouble(reader["regular_price"]);
                        product.Active = Convert.ToBoolean(reader["active"]);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return product;
        }
        public async Task<int[]> InsertProduct(Product product)
        {
            int result = 0, lastindex = -1;
            try
            {
                string extension = Path.GetExtension(product.ImageFile.FileName);
                product.DefaultImage = "product_default_img_" + product.Id.ToString() + DateTime.Now.ToString("yymmssfff") + extension;

                if (!await UploadImage(product.DefaultImage, product.ImageFile))
                {
                    result = -1;
                    return new int[] { result, lastindex };
                }
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string str = "insert into " +
                        "product(productname, brand_id, subcategory_id, gender, default_image, regular_price, sale_price, description)" +
                        "values (@productname, @brandid, @subcategoryid, @gender, @defaultimage, @regularprice, @saleprice, @description)";
                    MySqlCommand cmd = new MySqlCommand(str, conn);

                    cmd.Parameters.AddWithValue("productname", product.ProductName);
                    cmd.Parameters.AddWithValue("brandid", product.BrandId);
                    cmd.Parameters.AddWithValue("subcategoryid", product.SubCategoryId);
                    cmd.Parameters.AddWithValue("gender", product.Gender);
                    cmd.Parameters.AddWithValue("defaultimage", product.DefaultImage);
                    cmd.Parameters.AddWithValue("regularprice", product.RegularPrice);
                    cmd.Parameters.AddWithValue("saleprice", product.SalePrice);
                    cmd.Parameters.AddWithValue("description", product.Description);

                    result =  cmd.ExecuteNonQuery();
                    string getlastindex = "select Last_insert_id()";
                    cmd = new MySqlCommand(getlastindex, conn);
                    lastindex = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch(MySqlException e)
            {
                Console.WriteLine(e.Message);
                result = -1;
            }                
            return new int[] { result, lastindex};

        }
        public async Task<int> UpdateProduct(Product product)
        {
            try
            {
                Product oldProduct = GetProductById(product.Id);
                if (product.ImageFile != null)
                {
                    DeleteImage(oldProduct.DefaultImage);
                    string extension = Path.GetExtension(product.ImageFile.FileName);
                    product.DefaultImage = "product_default_img_" + product.Id.ToString() + DateTime.Now.ToString("yymmssfff") + extension;
                    if (!await UploadImage(product.DefaultImage, product.ImageFile))
                    {
                        return -1;
                    }
                }
                else
                {
                    product.DefaultImage = oldProduct.DefaultImage;
                }
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string str = "update product " +
                        "set productname = @productname, " +
                        "brand_id = @brandid, " +
                        "subcategory_id = @subcategoryid, " +
                        "gender = @gender," +
                        "default_image = @defaultimage, " + 
                        "regular_price = @regularprice, " +
                        "sale_price = @saleprice, " +
                        "description = @description " +
                        "where id = @id ";
                    MySqlCommand cmd = new MySqlCommand(str, conn);

                    cmd.Parameters.AddWithValue("id", product.Id);
                    cmd.Parameters.AddWithValue("productname", product.ProductName);
                    cmd.Parameters.AddWithValue("brandid", product.BrandId);
                    cmd.Parameters.AddWithValue("subcategoryid", product.SubCategoryId);
                    cmd.Parameters.AddWithValue("gender", product.Gender);
                    cmd.Parameters.AddWithValue("defaultimage", product.DefaultImage);
                    cmd.Parameters.AddWithValue("regularprice", product.RegularPrice);
                    cmd.Parameters.AddWithValue("saleprice", product.SalePrice);
                    cmd.Parameters.AddWithValue("description", product.Description);

                    return cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }

        }
        public int DeleteProduct(int id)
        {
            try
            {
                Product oldProduct = GetProductById(id);
                DeleteImage(oldProduct.DefaultImage);
                using (var conn = GetConnection())
                {
                    conn.Open();                    
                    List<ProductColorVariant> productColorVariants = GetColorVariantList(id);

                    foreach(var item in productColorVariants)
                    {
                        DeleteProductVariant(id, item.ColorId);
                    }
                    string str = "delete from " +
                        "product " +
                        "where id = @id";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("id", id);
                    int result = cmd.ExecuteNonQuery();

                    
                    //str = "delete from " +
                    //    "product_detail " +
                    //    "where product_id = @id";
                    //cmd = new MySqlCommand(str, conn);
                    //cmd.Parameters.AddWithValue("id", id);

                    
                    //str = "delete from " +
                    //    "product_color_variant " +
                    //    "where product_id = @id";
                    //cmd = new MySqlCommand(str, conn);
                    //cmd.Parameters.AddWithValue("id", id);
                    //cmd.ExecuteNonQuery();

                    return result;
                }
            }
            catch (MySqlException mysqle)
            {
                Console.WriteLine(mysqle.Message);
                try
                {
                    using (var conn = GetConnection())
                    {
                        conn.Open();
                        string str = "update " +
                            "product set active = 0 " +
                            "where id = @id";
                        MySqlCommand cmd = new MySqlCommand(str, conn);
                        cmd.Parameters.AddWithValue("id", id);
                        int result = cmd.ExecuteNonQuery();

                        //str = "update " +
                        //    "product_detail set active = 0 " +
                        //    "where product_id = @id";
                        //cmd = new MySqlCommand(str, conn);
                        //cmd.Parameters.AddWithValue("id", id);

                        //str = "update " +
                        //    "product_color_variant set active = 0 " +
                        //    "where product_id = @id";
                        //cmd = new MySqlCommand(str, conn);
                        //cmd.Parameters.AddWithValue("id", id);

                        return result;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return -1;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }
        public ProductMasterData GetProductMasterData(int id)
        {
            Product product = GetProductById(id);
            ProductMasterData productMasterData = new ProductMasterData(product);
            List<ProductColorVariant> colorVariants = GetColorVariantList(id);
            productMasterData.ProductVariants = new List<ProductVariantDetail>();

            foreach (var item in colorVariants)
            {
                ProductVariantDetail productVariantDetail = new ProductVariantDetail(item);
                productVariantDetail.ProductDetails = GetProductDetail(id, productVariantDetail.ColorId);
                productMasterData.ProductVariants.Add(productVariantDetail);
            }
            return productMasterData;
        }
        public List<ProductColorVariant> GetColorVariantList(int productId)
        {
            List<ProductColorVariant> variants = new List<ProductColorVariant>();
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string str = "select * from product_color_variant where product_id = @productId";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("productId", productId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            ProductColorVariant variant = new ProductColorVariant
                            {
                                ProductId = Convert.ToInt32(reader["product_id"]),
                                ColorId = Convert.ToInt32(reader["color_id"]),
                                ProductVariantImage = reader["product_variant_image"].ToString(),
                                Active = Convert.ToBoolean(reader["active"])
                            };
                            variants.Add(variant);
                        }                        
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return variants;
        }
        public List<ProductDetail> GetProductDetail(int productId, int colorId)
        {
            List<ProductDetail> list = new List<ProductDetail>();
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string str = "select * from product_detail where product_id = @productId and color_id = @colorId";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("productId", productId);
                    cmd.Parameters.AddWithValue("colorId", colorId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProductDetail detail = new ProductDetail
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                ProductId = Convert.ToInt32(reader["product_id"]),
                                ColorId = Convert.ToInt32(reader["color_id"]),
                                SizeId = Convert.ToInt32(reader["size_id"]),
                                Quantity = Convert.ToInt32(reader["quantity"]),
                                Active = Convert.ToBoolean(reader["active"])
                            };
                            list.Add(detail);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return list;
        }
        public ProductColorVariant GetProductVariantById(int productId, int colorId)
        {
            ProductColorVariant variant = new ProductColorVariant();
            try
            {
                using (var con = GetConnection())
                {
                    con.Open();
                    string str = "select * from product_color_variant where product_id = @productid and color_id = @colorid";
                    MySqlCommand cmd = new MySqlCommand(str, con);
                    cmd.Parameters.AddWithValue("productid", productId);
                    cmd.Parameters.AddWithValue("colorId", colorId);
                    using (var reader = cmd.ExecuteReader())
                    {

                        reader.Read();
                        variant.ProductId = Convert.ToInt32(reader["product_id"]);
                        variant.ColorId = Convert.ToInt32(reader["color_id"]);
                        variant.ProductVariantImage = Convert.ToString(reader["product_variant_image"]);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return variant;
        }
        public async Task<int> InsertColorVariant(ProductColorVariant variant)
        {
            try
            {
                string extension = Path.GetExtension(variant.ImageFile.FileName);
                variant.ProductVariantImage = "product_variant_" + variant.ProductId + "_color_" + variant.ColorId.ToString() + DateTime.Now.ToString("yymmssfff") + extension;

                if (!await UploadImage(variant.ProductVariantImage, variant.ImageFile))
                {
                    return -1;
                }

                using (var conn = GetConnection())
                {
                    conn.Open();
                    string str = "insert into " +
                        "product_color_variant (product_id, color_id, product_variant_image)" +
                        "values (@productId, @colorId, @variantImg)";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("productId", variant.ProductId);
                    cmd.Parameters.AddWithValue("colorId", variant.ColorId);
                    cmd.Parameters.AddWithValue("variantImg", variant.ProductVariantImage);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }
        public async Task<int> UpdateProductVariant(ProductColorVariant variant)
        {
            try
            {
                string oldVariantImage = GetProductVariantById(variant.ProductId, variant.ColorId).ProductVariantImage;
                if (variant.ImageFile != null)
                {
                    DeleteImage(oldVariantImage);
                    string extension = Path.GetExtension(variant.ImageFile.FileName);
                    variant.ProductVariantImage = "product_variant_" + variant.ProductId + "_color_" + variant.ColorId.ToString() + DateTime.Now.ToString("yymmssfff") + extension;
                    if (!await UploadImage(variant.ProductVariantImage, variant.ImageFile))
                    {
                        return -1;
                    }
                }
                else
                {
                    variant.ProductVariantImage = oldVariantImage;
                }
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string str = "update product_color_variant " +
                        "set product_variant_image = @colorImage " +
                        "where product_id = @productId and color_id = @colorId";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("productId", variant.ProductId);
                    cmd.Parameters.AddWithValue("colorId", variant.ColorId);
                    cmd.Parameters.AddWithValue("colorImage", variant.ProductVariantImage);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }
        public int DeleteProductVariant(int productId, int colorId)
        {
            try
            {
                var oldVariant = GetColorVariantList(productId).Where(v => v.ColorId == colorId).FirstOrDefault();
                using (var conn = GetConnection())
                {
                    DeleteImage(oldVariant.ProductVariantImage);
                    conn.Open();
                    string str = "delete from " +
                        "product_color_variant " +
                        "where product_id = @productId and color_id = @colorId";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("productId", productId);
                    cmd.Parameters.AddWithValue("colorId", colorId);
                    int result = cmd.ExecuteNonQuery();
                    str = "delete from " +
                        "product_detail " +
                        "where product_id = @productId and color_id = @colorId";
                    cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("productId", productId);
                    cmd.Parameters.AddWithValue("colorId", colorId);

                    cmd.ExecuteNonQuery();
                    return result;
                }
            }
            catch (MySqlException mysqle)
            {
                Console.WriteLine(mysqle.Message);
                try
                {
                    using (var conn = GetConnection())
                    {
                        conn.Open();
                        string str = "update " +
                            "product_color_variant set active = 0 " +
                            "where product_id = @productId and color_id = @colorId";
                        MySqlCommand cmd = new MySqlCommand(str, conn);
                        cmd.Parameters.AddWithValue("productId", productId);
                        cmd.Parameters.AddWithValue("colorId", colorId);
                        int result = cmd.ExecuteNonQuery();

                        str = "update " +
                            "product_detail set active = 0 " +
                            "where product_id = @productId and color_id = @colorId";
                        cmd = new MySqlCommand(str, conn);
                        cmd.Parameters.AddWithValue("productId", productId);
                        cmd.Parameters.AddWithValue("colorId", colorId);

                        return result;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return -1;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }
        public  int InsertProductDetail(ProductDetail productDetail)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    ProductDetail detail = GetProductDetail(productDetail.ProductId, productDetail.ColorId)
                        .Where<ProductDetail>(item => item.SizeId == productDetail.SizeId)
                        .FirstOrDefault();
                    if ( detail != null)
                    {
                        detail.Quantity += productDetail.Quantity;
                        UpdateProductDetail(detail);
                        return 1;
                    }    
                    conn.Open();
                    string str = "insert into " +
                        "product_detail (product_id, color_id, size_id, quantity)" +
                        "values (@productId, @colorId, @sizeId, @quantity)";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("productId", productDetail.ProductId);
                    cmd.Parameters.AddWithValue("colorId", productDetail.ColorId);
                    cmd.Parameters.AddWithValue("sizeId", productDetail.SizeId);
                    cmd.Parameters.AddWithValue("quantity", productDetail.Quantity);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }
        public int UpdateProductDetail(ProductDetail productDetail)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string str = "update " +
                        "product_detail set quantity = @quantity " +
                        "where id = @id";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("id", productDetail.Id);
                    cmd.Parameters.AddWithValue("quantity", productDetail.Quantity);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }
        public int DeleteProductDetail(int id)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string str = "delete from " +
                        "product_detail " +
                        "where id = @id";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("id", id);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch(MySqlException mysqle)
            {
                Console.WriteLine(mysqle.Message);
                try
                {
                    using (var conn = GetConnection())
                    {
                        conn.Open();
                        string str = "update " +
                            "product_detail set active = 0 " +
                            "where id = @id";
                        MySqlCommand cmd = new MySqlCommand(str, conn);
                        cmd.Parameters.AddWithValue("id", id);
                        return cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return -1;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }
        //Category CRUD - start
        public List<CategoryMasterModel> GetCategoryMasters()
        {
            List<CategoryMasterModel> list = new List<CategoryMasterModel>();
            foreach (var cate in GetCategories())
            {
                list.Add(new CategoryMasterModel
                {
                    Id = cate.Id,
                    CategoryName = cate.CategoryName,
                    SubCategoryList = GetSubCategoriesByCateId(cate.Id)
                });
            }
            return list;

        }
        public CategoryMasterModel GetCategoryMasterDataById(int id)
        {
            CategoryMasterModel result = new CategoryMasterModel();
            Category category = GetCategoryById(id);
            result.Id = category.Id;
            result.CategoryName = category.CategoryName;
            result.Active = category.Active;
            result.SubCategoryList = GetSubCategoriesByCateId(id);
            return result;
        }
        public List<Category> GetCategories()
        {
            List<Category> list = new List<Category>();
            using (var conn = GetConnection())
            {
                conn.Open();
                string str = "select * from category";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Category
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            CategoryName = Convert.ToString(reader["categoryName"]),
                            Active = Convert.ToBoolean(reader["active"])
                        });
                    }
                }
            }
            return list;
        }
        public Category GetCategoryById(int id)
        {
            Category category = new Category();
            using (var conn = GetConnection())
            {
                conn.Open();
                string str = "select * from category where id = @id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    category.Id = Convert.ToInt32(reader["id"]);
                    category.CategoryName = Convert.ToString(reader["categoryName"]);
                    category.Active = Convert.ToBoolean(reader["active"]);
                }
            }
            return category;
        }
        public Dictionary<string, int> InsertCategory(Category category)
        {
            Dictionary<string, int> resultDict = new Dictionary<string, int>();
            int result = 0, lastIdx = -1;
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string str = "insert into " +
                        "category(categoryName)" +
                        "values (@categoryName)";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("categoryName", category.CategoryName);
                    result = cmd.ExecuteNonQuery();
                    string getlastindex = "select Last_insert_id()";
                    cmd = new MySqlCommand(getlastindex, conn);
                    lastIdx = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                result = -1;
            }
            resultDict.Add("result", result);
            resultDict.Add("id", lastIdx);
            return resultDict;
        }
        public int UpdateCategory(Category category)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string str = "update category " +
                        "set categoryname = @categoryName " +
                        "where id = @id";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("categoryName", category.CategoryName);
                    cmd.Parameters.AddWithValue("id", category.Id);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return -1;
            }
        }
        public int DeleteCategory(int id)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string str = "delete " +
                        "from category " +
                        "where id = @id";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("id", id);
                    int result = cmd.ExecuteNonQuery();

                    str = "delete " +
                        "from subcategory " +
                        "where category_id = @id";
                    cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.ExecuteNonQuery();
                    return result;
                }
            }
            catch (MySqlException mysqle)
            {
                Console.WriteLine(mysqle);
                try
                {
                    using (var conn = GetConnection())
                    {
                        conn.Open();
                        string str = "update category " +
                            "set active = 0 " +
                            "where id = @id";
                        MySqlCommand cmd = new MySqlCommand(str, conn);
                        cmd.Parameters.AddWithValue("id", id);
                        int result = cmd.ExecuteNonQuery();

                        str = "update subcategory " +
                            "set active = 0 " +
                            "where id = @id";
                        cmd = new MySqlCommand(str, conn);
                        cmd.Parameters.AddWithValue("id", id);
                        cmd.ExecuteNonQuery();

                        return result;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return -1;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }
        public List<SubCategory> GetSubCategoriesByCateId(int id)
        {
            List<SubCategory> list = new List<SubCategory>();
            using (var conn = GetConnection())
            {
                conn.Open();
                string str = "select * from SubCategory where category_id = @id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new SubCategory
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            SubCategoryName = Convert.ToString(reader["subcategory_name"]),
                            CategoryId = Convert.ToInt32(reader["category_id"]),
                            Active = Convert.ToBoolean(reader["active"])
                        });
                    }
                }
            }
            return list;
        }
        public List<SubCategory> GetSubCategories()
        {
            List<SubCategory> list = new List<SubCategory>();
            using (var conn = GetConnection())
            {
                conn.Open();
                string str = "select * from SubCategory";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new SubCategory
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            SubCategoryName = Convert.ToString(reader["subcategory_name"]),
                            CategoryId = Convert.ToInt32(reader["category_id"]),
                            Active = Convert.ToBoolean(reader["active"])
                        });
                    }
                }
            }
            return list;
        }
        public SubCategory GetSubCategoryById(int id)
        {
            SubCategory subCategory = new SubCategory();
            using (var conn = GetConnection())
            {
                conn.Open();
                string str = "select * from SubCategory where id = @id";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();

                    subCategory.Id = Convert.ToInt32(reader["id"]);
                    subCategory.SubCategoryName = Convert.ToString(reader["subcategory_name"]);
                    subCategory.CategoryId = Convert.ToInt32(reader["category_id"]);
                    subCategory.Active = Convert.ToBoolean(reader["active"]);

                }
            }
            return subCategory;
        }
        public int InsertSubCategory(SubCategory subCategory)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string str = "insert into subcategory(subcategory_name, category_id) " +
                        "values(@subcategoryName, @categoryId)";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("subcategoryName", subCategory.SubCategoryName);
                    cmd.Parameters.AddWithValue("categoryId", subCategory.CategoryId);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return -1;
            }
        }
        public int UpdateSubCategory(SubCategory subCategory)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string str = "update subcategory " +
                        "set subcategory_name = @subcategoryName " +
                        "where id = @id";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("subcategoryName", subCategory.SubCategoryName);
                    cmd.Parameters.AddWithValue("id", subCategory.Id);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return -1;
            }
        }
        public int DeleteSubCategory(int id)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string str = "delete " +
                        "from subcategory " +
                        "where id = @id";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("id", id);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException mysqle)
            {
                Console.WriteLine(mysqle);
                try
                {
                    using (var conn = GetConnection())
                    {
                        conn.Open();
                        string str = "update subcategory " +
                            "set active = 0 " +
                            "where id = @id";
                        MySqlCommand cmd = new MySqlCommand(str, conn);
                        cmd.Parameters.AddWithValue("id", id);
                        return cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return -1;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }
        //Category CRUD - end

        //Brand CRUD - start
        public List<Brand> GetBrands()
        {
            List<Brand> list = new List<Brand>();
            using (var conn = GetConnection())
            {
                conn.Open();
                string str = "select * from brand";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Brand
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            BrandName = Convert.ToString(reader["brand_name"]),
                            Active = Convert.ToBoolean(reader["active"])
                        });
                    }
                }
            }
            return list;
        }
        public Brand GetBrandById(int id)
        {
            Brand brand = new Brand();
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string str = "select * from brand where id = @id";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        brand.Id = Convert.ToInt32(reader["id"]);
                        brand.BrandName = Convert.ToString(reader["brand_name"]);
                        brand.Active = Convert.ToBoolean(reader["active"]);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return brand;
        }
        public int InsertBrand(Brand brand)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string str = "insert into " +
                        "brand(id, brand_name) " +
                        "value(@id, @brandName)";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("id", brand.Id);
                    cmd.Parameters.AddWithValue("brandName", brand.BrandName);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }
        public int UpdateBrand(Brand brand)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string str = "update brand " +
                        "set brand_name = @brandName " +
                        "where id = @id";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("id", brand.Id);
                    cmd.Parameters.AddWithValue("brandName", brand.BrandName);
                    cmd.Parameters.AddWithValue("brandName", brand.BrandName);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }

        public int DeleteBrand(int id)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string str = "delete " +
                        "from brand " +
                        "where id = @id";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("id", id);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException mysqle)
            {
                Console.WriteLine(mysqle.Message);
                try
                {
                    using (var conn = GetConnection())
                    {
                        conn.Open();
                        string str = "update brand " +
                            "set active = 0 " +
                            "where id = @id";
                        MySqlCommand cmd = new MySqlCommand(str, conn);
                        cmd.Parameters.AddWithValue("id", id);
                        return cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return -1;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }
        //Brand CRUD - end

        //Size CRUD - start
        public List<Size> GetSizes()
        {
            List<Size> list = new List<Size>();
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string str = "select * from size";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Size
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                SizeName = Convert.ToString(reader["size_name"]),
                                Active = Convert.ToBoolean(reader["active"])
                            });
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            return list;
        }
        public Size GetSizeById(int id)
        {
            Size size = new Size();
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string str = "select * from size where id = @id";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        size.Id = Convert.ToInt32(reader["id"]);
                        size.SizeName = Convert.ToString(reader["size_name"]);
                        size.Active = Convert.ToBoolean(reader["active"]);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return size;
        }
        public int InsertSize(Size size)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string str = "insert into " +
                        "size(id, size_name) " +
                        "value(@id, @sizeName)";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("id", size.Id);
                    cmd.Parameters.AddWithValue("sizeName", size.SizeName);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }
        public int UpdateSize(Size size)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string str = "update size " +
                        "set size_name = @sizeName " +
                        "where id = @id";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("id", size.Id);
                    cmd.Parameters.AddWithValue("sizeName", size.SizeName);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }

        public int DeleteSize(int id)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string str = "delete " +
                        "from size " +
                        "where id = @id";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("id", id);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException mysqle)
            {
                Console.WriteLine(mysqle.Message);
                try
                {
                    using (var conn = GetConnection())
                    {
                        conn.Open();
                        string str = "update size " +
                            "set active = 0 " +
                            "where id = @id";
                        MySqlCommand cmd = new MySqlCommand(str, conn);
                        cmd.Parameters.AddWithValue("id", id);
                        return cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return -1;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }
        //Size CRUD - end

        //Color CRUD - start
        public List<Color> GetColors()
        {
            List<Color> listColor = new List<Color>();
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string str = "select * from color";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listColor.Add(new Color
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                ColorName = Convert.ToString(reader["color_name"]),
                                ColorImage = Convert.ToString(reader["color_image"]),
                                Active = Convert.ToBoolean(reader["active"])
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return listColor;
        }
        public Color GetColorById(int id)
        {
            Color color = new Color();
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string str = "select * from color where id = @id";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        color.Id = Convert.ToInt32(reader["id"]);
                        color.ColorName = Convert.ToString(reader["color_name"]);
                        color.ColorImage = Convert.ToString(reader["color_image"]);
                        color.Active = Convert.ToBoolean(reader["active"]);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }                
            return color;

        }
        public async Task<int> InsertColorAsync(Color color)
        {
            try
            {       
                string extension = Path.GetExtension(color.ImageFile.FileName);
                color.ColorImage = "color_" + color.ColorName + DateTime.Now.ToString("yymmssfff") + extension;

                if (! await UploadImage(color.ColorImage, color.ImageFile))
                {
                    return -1;
                }

                using (var conn = GetConnection())
                {
                    conn.Open();
                    string str = "insert into " +
                        "color (color_name, color_image)" +
                        "values (@colorName, @colorImage)";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("colorName", color.ColorName);
                    cmd.Parameters.AddWithValue("colorImage", color.ColorImage);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }

        public async Task<int> UpdateColorAsync(Color color)
        {
            try
            {
                Color oldColor = GetColorById(color.Id);
                if (color.ImageFile != null)
                {
                    DeleteImage(oldColor.ColorImage);
                    string extension = Path.GetExtension(color.ImageFile.FileName);
                    color.ColorImage = "color_" + color.ColorName + DateTime.Now.ToString("yymmssfff") + extension;
                    if (! await UploadImage(color.ColorImage, color.ImageFile) )
                    {
                        return -1;
                    }
                }
                else
                {
                    color.ColorImage = oldColor.ColorImage;
                }
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string str = "update color " +
                        "set color_name = @colorName, color_image = @colorImage " +
                        "where id = @id";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("colorName", color.ColorName);
                    cmd.Parameters.AddWithValue("id", color.Id);
                    cmd.Parameters.AddWithValue("colorImage", color.ColorImage);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }
        public int DeleteColor(int id)
        {                
            Color color = GetColorById(id);
            try
            {
                DeleteImage(color.ColorImage);
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string str = "delete " +
                        "from color " +
                        "where id = @id";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("id", color.Id);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch(MySqlException mysqle)
            {
                Console.WriteLine(mysqle);
                try
                {
                    using (var conn = GetConnection())
                    {
                        conn.Open();
                        string str = "update color " +
                            "set active = 0 " +
                            "where id = @id";
                        MySqlCommand cmd = new MySqlCommand(str, conn);
                        cmd.Parameters.AddWithValue("id", color.Id);
                        return cmd.ExecuteNonQuery();
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    return -1;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }
        //Color CRUD - end 

        //image upload method - start
        public async Task<bool> UploadImage(string imageName, IFormFile imageFile)
        {
            try
            {
                string rootPath = _rootPath;
                string path = Path.Combine(rootPath + "/Image/", imageName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }
        public bool DeleteImage(string imageName)
        {
            try
            {
                string rootPath = _rootPath;
                string absoluteFilePath = Path.Combine(rootPath + "/image/", imageName);
                FileInfo file = new FileInfo(absoluteFilePath);
                if (file.Exists)
                {
                    file.Delete();
                }
                else return false;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }
        public bool UpdateImageName(string oldName, string newName)
        {
            try
            {
                string rootPath = _rootPath;
                string oldFilePath = Path.Combine(rootPath + "/image/", oldName);
                string newFilePath = Path.Combine(rootPath + "/image/", newName);
                File.Move(oldFilePath, newFilePath);
            
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }
        //image upload method - end
 



        /*public int InsertIn4(User usr)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open(); 
                var str = "insert into user (fullname,email,password) values(@FullName, @EMail, @PAssword)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("FullName", usr.fullname);
                cmd.Parameters.AddWithValue("EMail", usr.email);
                cmd.Parameters.AddWithValue("PAssword", usr.password);
                return (cmd.ExecuteNonQuery());
            }
        }*/
        public int InsertIn4(User usr)
        {
            //checking if user already exist
            if (!IsUserExist(usr.email))
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    var str = "insert into user (fullname,email,password) values(@FullName, @EMail, @PAssword)";
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("FullName", usr.fullname);
                    cmd.Parameters.AddWithValue("EMail", usr.email);
                    cmd.Parameters.AddWithValue("PAssword", usr.password);
                    return (cmd.ExecuteNonQuery());
                }
            }
            else
            {
                return 0;
            }
        }
        private bool IsUserExist(string email)
        {
            bool IsUserExist = false;
            string str = "select * from user where email=@email";
            using (MySqlConnection conn = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("@email", email);
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                if (dt.Rows.Count > 0)
                {
                    IsUserExist = true;
                }
            }
            return IsUserExist;
        }
        public List<User> LogIn(string email, string password)
        {
            List<User> list = new List<User>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from user where email=@email and password=@password";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("email", email);
                cmd.Parameters.AddWithValue("password", password);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            list.Add(new User()
                            {
                                fullname = reader["email"].ToString(),
                                password = reader["password"].ToString(),
                            });
                        }
                        reader.Close();
                    }
                }
                conn.Close();
                //phần mới thêm
            }
            return list;
        }




                public List<object> GetColorOfProduct()
                {

                    List<object> list = new List<object>();

                    using (MySqlConnection conn = GetConnection())
                    {
                        conn.Open();

                        string str = "select distinct ProductName, Color_name, Size_name  from Product p,Color c, Product_Color_variant v, Product_detail d, Size s " +
                        "where p.Id= d.Product_Id and d.Color_Id=c.Id and d.Size_Id=s.Id";


                        MySqlCommand cmd = new MySqlCommand(str, conn);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var ob = new
                                {
                                    Productname = reader["ProductName"].ToString(),
                                    Colorname = reader["Color_name"].ToString(),
                                    Sizename = reader["Size_name"].ToString()
                                };
                                list.Add(ob);

                            }
                            reader.Close();
                        }

                        conn.Close();

                    }
                    return list;


                }

                public List<Product> TimSanPhamTheoTen(string ten)
                {
                    //int i = 0;
                    List<Product> list = new List<Product>();
                    using (MySqlConnection conn = GetConnection())
                    {
                        conn.Open();
                        var str = "select * from Product where Productname LIKE CONCAT('%', @tensp, '%')";
                        MySqlCommand cmd = new MySqlCommand(str, conn);
                        cmd.Parameters.AddWithValue("tensp", ten);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Product()
                                {
                                    ProductName = reader["ProductName"].ToString(),
                                    DefaultImage = reader["default_image"].ToString(),
                                    SalePrice = Convert.ToInt32(reader["sale_price"]),


                                });
                            }
                        }
                    }
                    return list;
                }


                public List<Product> GetProducts(int IdColor)
                {
                    //int i = 0;
                    List<Product> list = new List<Product>();
                    using (MySqlConnection conn = GetConnection())
                    {
                        conn.Open();
                        var str = "select distinct p.Id, ProductName from Product p,Color c,Product_detail a where p.Id= a.Product_Id and " +
                            " a.Color_Id=@IdColor";
                        MySqlCommand cmd = new MySqlCommand(str, conn);
                        cmd.Parameters.AddWithValue("IdColor", IdColor);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Product()
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    ProductName = reader["ProductName"].ToString(),


                                });
                            }
                        }
                    }
                    return list;
                }
                public List<Product> GetProductNew()
                {
                    //int i = 0;
                    List<Product> list = new List<Product>();
                    using (MySqlConnection conn = GetConnection())
                    {
                        conn.Open();
                        var str = "select * from Product where Product_new = 1";
                        MySqlCommand cmd = new MySqlCommand(str, conn);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Product()
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    ProductName = reader["ProductName"].ToString(),
                                    SalePrice = Convert.ToInt64(reader["sale_price"]),
                                    DefaultImage = reader["default_image"].ToString(),
                                    ProductNew = Convert.ToBoolean(reader["Product_new"]),

                                });
                            }
                        }
                    }
                    return list;
                }
                public List<Product> GetProduct()
                {
                    //int i = 0;
                    List<Product> list = new List<Product>();
                    using (MySqlConnection conn = GetConnection())
                    {
                        conn.Open();
                        var str = "select * from Product";
                        MySqlCommand cmd = new MySqlCommand(str, conn);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Product()
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    ProductName = reader["ProductName"].ToString(),
                                    SalePrice = Convert.ToInt64(reader["sale_price"]),
                                    DefaultImage = reader["default_image"].ToString(),
                                    ProductNew = Convert.ToBoolean(reader["Product_new"]),

                                });
                            }
                        }
                    }
                    return list;
                }
                public List<Product> GetProductBestSeller()
                {
                    //int i = 0;
                    List<Product> list = new List<Product>();
                    using (MySqlConnection conn = GetConnection())
                    {
                        conn.Open();
                        var str = "SELECT Productname, sale_price, default_image, p.Id as Id from Product p, Product_detail d, order_detail o " +
                            "where p.Id = d.Product_Id and d.Id = o.Product_detail_Id group by Productname order by count(o.Product_detail_Id) desc";
                        MySqlCommand cmd = new MySqlCommand(str, conn);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Product()
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    ProductName = reader["ProductName"].ToString(),
                                    SalePrice = Convert.ToInt64(reader["sale_price"]),
                                    DefaultImage = reader["default_image"].ToString(),


                                });
                            }
                        }
                    }
                    return list;
                }
                public List<Product> GetPriceASC()
                {
                    //int i = 0;
                    List<Product> list = new List<Product>();
                    using (MySqlConnection conn = GetConnection())
                    {
                        conn.Open();
                        var str = "select * from Product order by sale_price asc";
                        MySqlCommand cmd = new MySqlCommand(str, conn);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Product()
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    ProductName = reader["ProductName"].ToString(),
                                    SalePrice = Convert.ToInt64(reader["sale_price"]),
                                    DefaultImage = reader["default_image"].ToString(),


                                });
                            }
                        }
                    }
                    return list;
                }

                public List<Product> GetPriceDESC()
                {
                    //int i = 0;
                    List<Product> list = new List<Product>();
                    using (MySqlConnection conn = GetConnection())
                    {
                        conn.Open();
                        var str = "select * from Product order by sale_price desc";
                        MySqlCommand cmd = new MySqlCommand(str, conn);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Product()
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    ProductName = reader["ProductName"].ToString(),
                                    SalePrice = Convert.ToInt64(reader["sale_price"]),
                                    DefaultImage = reader["default_image"].ToString(),


                                });
                            }
                        }
                    }
                    return list;
                }
                public List<Product> GetProductPopular()
                {
                    //int i = 0;
                    List<Product> list = new List<Product>();
                    using (MySqlConnection conn = GetConnection())
                    {
                        conn.Open();
                        var str = "select * from Product where active = 1";
                        MySqlCommand cmd = new MySqlCommand(str, conn);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Product()
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    ProductName = reader["ProductName"].ToString(),
                                    SalePrice = Convert.ToInt64(reader["sale_price"]),
                                    DefaultImage = reader["default_image"].ToString(),


                                });
                            }
                        }
                    }
                    return list;
                }


                public List<Product> GetProductWoman()
                {
                    //int i = 0;
                    List<Product> list = new List<Product>();
                    using (MySqlConnection conn = GetConnection())
                    {
                        conn.Open();
                        var str = "select * from Product where gender = 0";
                        MySqlCommand cmd = new MySqlCommand(str, conn);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Product()
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    ProductName = reader["ProductName"].ToString(),
                                    SalePrice = Convert.ToInt64(reader["sale_price"]),
                                    DefaultImage = reader["default_image"].ToString(),


                                });
                            }
                        }
                    }
                    return list;
                }


                public List<Product> GetProductsBaseSize(int IdSize)
                {
                    //int i = 0;
                    List<Product> list = new List<Product>();
                    using (MySqlConnection conn = GetConnection())
                    {
                        conn.Open();
                        var str = "select distinct p.Id, ProductName from Product p,Color c,Product_detail a where p.Id= a.Product_Id and " +
                            " a.Size_Id=@IdColor";
                        MySqlCommand cmd = new MySqlCommand(str, conn);
                        cmd.Parameters.AddWithValue("IdColor", IdSize);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Product()
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    ProductName = reader["ProductName"].ToString(),


                                });
                            }
                        }
                    }
                    return list;
                }

                public List<Brand> GetBrand()
                {
                    //int i = 0;
                    List<Brand> list = new List<Brand>();
                    using (MySqlConnection conn = GetConnection())
                    {
                        conn.Open();
                        var str = "select *from Brand";
                        MySqlCommand cmd = new MySqlCommand(str, conn);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Brand()
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    BrandName = reader["Brand_name"].ToString(),
                                    Active = Convert.ToBoolean(reader["active"])


                                });
                            }
                        }
                    }
                    return list;
                }

                public List<Product> GetProductBrand(int IdBrand)
                {
                    //int i = 0;
                    List<Product> list = new List<Product>();
                    using (MySqlConnection conn = GetConnection())
                    {
                        conn.Open();
                        var str = "select distinct p.Id, ProductName from Product p,Product_detail a, Brand b where p.Id= a.Product_Id and " +
                            " p.Brand_Id=@IdBrand";
                        MySqlCommand cmd = new MySqlCommand(str, conn);
                        cmd.Parameters.AddWithValue("IdBrand", IdBrand);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new Product()
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    ProductName = reader["ProductName"].ToString(),


                                });
                            }
                        }
                    }
                    return list;
                }           
    }
}
