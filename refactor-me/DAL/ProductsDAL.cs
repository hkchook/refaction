using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using refactor_me.Models;

namespace refactor_me.DAL
{
    public class ProductsDAL
    {
        public static List<Product> GetAll(string connectionString)
        {
            List<Product> pl = null;

            using (SqlConnection sqlconnection = new SqlConnection(connectionString))
            {
                sqlconnection.Open();
                using (SqlCommand sqlcommand = new SqlCommand())
                {
                    sqlcommand.Connection = sqlconnection;
                    sqlcommand.CommandType = CommandType.StoredProcedure;
                    sqlcommand.CommandText = "PRODUCT_SELECTALL";

                    using (SqlDataReader dr = sqlcommand.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Product product = ReadProduct(dr);

                            if (pl == null)
                            {
                                pl = new List<Product>();
                            }

                            pl.Add(product);
                        }
                    }
                }
            }

            return pl;
        }

        public static Product GetProduct(string connectionString, string name)
        {
            Product product = null;

            using (SqlConnection sqlconnection = new SqlConnection(connectionString))
            {
                sqlconnection.Open();
                using (SqlCommand sqlcommand = new SqlCommand())
                {
                    sqlcommand.Connection = sqlconnection;
                    sqlcommand.CommandType = CommandType.StoredProcedure;
                    sqlcommand.CommandText = "PRODUCT_SELECTBYNAME";
                    sqlcommand.Parameters.Add("@NAME", SqlDbType.NVarChar).Value = name;

                    using (SqlDataReader dr = sqlcommand.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            product = ReadProduct(dr);
                        }
                    }
                }
            }

            return product;
        }

        public static Product GetProduct(string connectionString, Guid id)
        {
            Product product = null;

            using (SqlConnection sqlconnection = new SqlConnection(connectionString))
            {
                sqlconnection.Open();
                using (SqlCommand sqlcommand = new SqlCommand())
                {
                    sqlcommand.Connection = sqlconnection;
                    sqlcommand.CommandType = CommandType.StoredProcedure;
                    sqlcommand.CommandText = "PRODUCT_SELECTBYID";
                    sqlcommand.Parameters.Add("@ID", SqlDbType.NVarChar).Value = id.ToString();

                    using (SqlDataReader dr = sqlcommand.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            product = ReadProduct(dr);
                        }
                    }
                }
            }

            return product;
        }

        public static List<ProductOption> GetProductOptions(string connectionString, Guid productId)
        {
            List<ProductOption> pl = null;

            using (SqlConnection sqlconnection = new SqlConnection(connectionString))
            {
                sqlconnection.Open();
                using (SqlCommand sqlcommand = new SqlCommand())
                {
                    sqlcommand.Connection = sqlconnection;
                    sqlcommand.CommandType = CommandType.StoredProcedure;
                    sqlcommand.CommandText = "PRODUCTOPTION_SELECTBYPRODUCTID";
                    sqlcommand.Parameters.Add("@PRODUCTID", SqlDbType.NVarChar).Value = productId.ToString();

                    using (SqlDataReader dr = sqlcommand.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ProductOption productoption = ReadProductOption(dr);

                            if (pl == null)
                            {
                                pl = new List<ProductOption>();
                            }

                            pl.Add(productoption);
                        }
                    }
                }
            }

            return pl;
        }

        public static ProductOption GetProductOption(string connectionString, Guid productId, Guid id)
        {
            ProductOption productoption = null;

            using (SqlConnection sqlconnection = new SqlConnection(connectionString))
            {
                sqlconnection.Open();
                using (SqlCommand sqlcommand = new SqlCommand())
                {
                    sqlcommand.Connection = sqlconnection;
                    sqlcommand.CommandType = CommandType.StoredProcedure;
                    sqlcommand.CommandText = "PRODUCTOPTION_SELECTBYPRODUCTIDANDID";
                    sqlcommand.Parameters.Add("@PRODUCTID", SqlDbType.NVarChar).Value = productId.ToString();
                    sqlcommand.Parameters.Add("@ID", SqlDbType.NVarChar).Value = id.ToString();

                    using (SqlDataReader dr = sqlcommand.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            productoption = ReadProductOption(dr);
                        }
                    }
                }
            }

            return productoption;
        }

        public static bool CreateProduct(string connectionString, Product product)
        {
            bool updated = false;

            using (SqlConnection sqlconnection = new SqlConnection(connectionString))
            {
                sqlconnection.Open();
                using (SqlCommand sqlcommand = new SqlCommand())
                {
                    sqlcommand.Connection = sqlconnection;
                    sqlcommand.CommandType = CommandType.StoredProcedure;
                    sqlcommand.CommandText = "PRODUCT_INSERT";
                    sqlcommand.Parameters.Add("@ID", SqlDbType.NVarChar).Value = product.Id.ToString();
                    sqlcommand.Parameters.Add("@NAME", SqlDbType.NVarChar).Value = product.Name;
                    sqlcommand.Parameters.Add("@DESC", SqlDbType.NVarChar).Value = product.Description;
                    sqlcommand.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = product.Price;
                    sqlcommand.Parameters.Add("@DELIVERYPRICE", SqlDbType.Decimal).Value = product.DeliveryPrice;

                    int iupdated = sqlcommand.ExecuteNonQuery();

                    if (iupdated > 0)
                    {
                        updated = true;
                    }
                }
            }

            return updated;
        }

        public static bool UpdateProduct(string connectionString, Guid id, Product product)
        {
            bool updated = false;

            using (SqlConnection sqlconnection = new SqlConnection(connectionString))
            {
                sqlconnection.Open();
                using (SqlCommand sqlcommand = new SqlCommand())
                {
                    sqlcommand.Connection = sqlconnection;
                    sqlcommand.CommandType = CommandType.StoredProcedure;
                    sqlcommand.CommandText = "PRODUCT_UPDATE";
                    sqlcommand.Parameters.Add("@ID", SqlDbType.NVarChar).Value = id.ToString();
                    sqlcommand.Parameters.Add("@NAME", SqlDbType.NVarChar).Value = product.Name;
                    sqlcommand.Parameters.Add("@DESC", SqlDbType.NVarChar).Value = product.Description;
                    sqlcommand.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = product.Price;
                    sqlcommand.Parameters.Add("@DELIVERYPRICE", SqlDbType.Decimal).Value = product.DeliveryPrice;

                    int iupdated = sqlcommand.ExecuteNonQuery();

                    if (iupdated > 0)
                    {
                        updated = true;
                    }
                }
            }

            return updated;
        }

        public static bool DeleteProduct(string connectionString, Guid id)
        {
            bool updated = false;

            using (SqlConnection sqlconnection = new SqlConnection(connectionString))
            {
                sqlconnection.Open();
                using (SqlCommand sqlcommand = new SqlCommand())
                {
                    sqlcommand.Connection = sqlconnection;
                    sqlcommand.CommandType = CommandType.StoredProcedure;
                    sqlcommand.CommandText = "PRODUCT_DELETE";
                    sqlcommand.Parameters.Add("@ID", SqlDbType.NVarChar).Value = id.ToString();

                    int iupdated = sqlcommand.ExecuteNonQuery();

                    if (iupdated > 0)
                    {
                        updated = true;
                    }
                }
            }

            return updated;
        }

        public static bool CreateOption(string connectionString, Guid productId, ProductOption option)
        {
            bool updated = false;

            using (SqlConnection sqlconnection = new SqlConnection(connectionString))
            {
                sqlconnection.Open();
                using (SqlCommand sqlcommand = new SqlCommand())
                {
                    sqlcommand.Connection = sqlconnection;
                    sqlcommand.CommandType = CommandType.StoredProcedure;
                    sqlcommand.CommandText = "PRODUCTOPTION_INSERT";
                    sqlcommand.Parameters.Add("@PRODUCTID", SqlDbType.NVarChar).Value = productId.ToString();
                    sqlcommand.Parameters.Add("@ID", SqlDbType.NVarChar).Value = option.Id.ToString();
                    sqlcommand.Parameters.Add("@NAME", SqlDbType.NVarChar).Value = option.Name;
                    sqlcommand.Parameters.Add("@DESC", SqlDbType.NVarChar).Value = option.Description;

                    int iupdated = sqlcommand.ExecuteNonQuery();

                    if (iupdated > 0)
                    {
                        updated = true;
                    }
                }
            }

            return updated;
        }

        public static bool UpdateOption(string connectionString, Guid productId, Guid id, ProductOption option)
        {
            bool updated = false;

            using (SqlConnection sqlconnection = new SqlConnection(connectionString))
            {
                sqlconnection.Open();
                using (SqlCommand sqlcommand = new SqlCommand())
                {
                    sqlcommand.Connection = sqlconnection;
                    sqlcommand.CommandType = CommandType.StoredProcedure;
                    sqlcommand.CommandText = "PRODUCTOPTION_UPDATE";
                    sqlcommand.Parameters.Add("@PRODUCTID", SqlDbType.NVarChar).Value = productId.ToString();
                    sqlcommand.Parameters.Add("@ID", SqlDbType.NVarChar).Value = id.ToString();
                    sqlcommand.Parameters.Add("@NAME", SqlDbType.NVarChar).Value = option.Name;
                    sqlcommand.Parameters.Add("@DESC", SqlDbType.NVarChar).Value = option.Description;

                    int iupdated = sqlcommand.ExecuteNonQuery();

                    if (iupdated > 0)
                    {
                        updated = true;
                    }
                }
            }

            return updated;
        }

        public static bool DeleteOption(string connectionString, Guid id)
        {
            bool updated = false;

            using (SqlConnection sqlconnection = new SqlConnection(connectionString))
            {
                sqlconnection.Open();
                using (SqlCommand sqlcommand = new SqlCommand())
                {
                    sqlcommand.Connection = sqlconnection;
                    sqlcommand.CommandType = CommandType.StoredProcedure;
                    sqlcommand.CommandText = "PRODUCTOPTION_DELETE";
                    sqlcommand.Parameters.Add("@ID", SqlDbType.NVarChar).Value = id.ToString();

                    int iupdated = sqlcommand.ExecuteNonQuery();

                    if (iupdated > 0)
                    {
                        updated = true;
                    }
                }
            }

            return updated;
        }

        private static Product ReadProduct(SqlDataReader dr)
        {
            decimal price = 0.0m;
            decimal deliveryprice = 0.0m;

            decimal.TryParse(dr["Price"].ToString(), out price);
            decimal.TryParse(dr["DeliveryPrice"].ToString(), out deliveryprice);

            Product product = new Product
            {
                Id = new Guid(dr["Id"].ToString()),
                Name = dr["Name"].ToString(),
                Description = dr["Description"].ToString(),
                Price = price,
                DeliveryPrice = deliveryprice
            };

            return product;
        }

        private static ProductOption ReadProductOption(SqlDataReader dr)
        {
            ProductOption productoption = new ProductOption
            {
                Id = new Guid(dr["Id"].ToString()),
                Name = dr["Name"].ToString(),
                Description = dr["Description"].ToString(),
                ProductId = new Guid(dr["productId"].ToString())
            };

            return productoption;
        }
    }
}