using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoesLover.Models;
namespace ShoesLover.Data
{
    public class StoreContext
    {
        public string ConnectionString { get; set; }
        public StoreContext(string connString)
        {
            ConnectionString = connString;
        }
        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
        public List<Color> GetColors()
        {
            List<Color> listColor = new List<Color>();
            using (var conn = GetConnection())
            {
                conn.Open();
                string str = "select * from color";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader() )
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
            return listColor;
        }
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
                            SubCategoryName = Convert.ToString(reader["brand_name"]),
                            CategoryId = Convert.ToInt32(reader["category_id"]),
                            Active = Convert.ToBoolean(reader["active"])
                        });
                    }
                }
            }
            return list;
        }
        public List<Size> GetSizes()
        {
            List<Size> list = new List<Size>();
            using (var conn = GetConnection())
            {
                conn.Open();
                string str = "select * from Size";
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
            return list;
        }
        public List<Product> GetProducts()
        {
            List<Product> listProduct = new List<Product>();
            try
            {
                using (var con = GetConnection())
                {   con.Open();
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
    }
}
