using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using ShoesLover.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

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
                    string str = "select * from product";
                    MySqlCommand cmd = new MySqlCommand(str, con);
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
        public int[] InsertProduct(Product product)
        {
            int result = 0, lastindex = -1;
            try
            {
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
        public ProductMasterData GetProductMasterData(int id)
        {
            Product product = GetProductById(id);
            ProductMasterData productMasterData = new ProductMasterData(product);
            List<ProductColorVariant> colorVariants = GetColorVariantList(id);
            foreach(var item in colorVariants)
            {
                ProductVariantDetail productVariantDetail = new ProductVariantDetail(item);
                productMasterData.ProductVariants = new List<ProductVariantDetail>();
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
                                SizeId = Convert.ToInt32(reader["sizeId"]),
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

    }
}
