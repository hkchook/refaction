using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using refactor_me.DAL;

namespace refactor_me.Models
{
    public class ProductsBLL
    {
        private readonly string connectionString;

        public ProductsBLL()
        {
            connectionString = (ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString).Replace("{DataDirectory}", HttpContext.Current.Server.MapPath("~/App_Data"));
        }

        public List<Product> GetAll()
        {
            return ProductsDAL.GetAll(connectionString);
        }

        public Product GetProduct(string name)
        {
            return ProductsDAL.GetProduct(connectionString, name);
        }

        public Product GetProduct(Guid id)
        {
            return ProductsDAL.GetProduct(connectionString, id);
        }

        public List<ProductOption> GetProductOptions(Guid productId)
        {
            return ProductsDAL.GetProductOptions(connectionString, productId);
        }

        public ProductOption GetProductOption(Guid productId, Guid id)
        {
            return ProductsDAL.GetProductOption(connectionString, productId, id);
        }

        public bool CreateProduct(Product product)
        {
            return ProductsDAL.CreateProduct(connectionString, product);
        }

        public bool UpdateProduct(Guid id, Product product)
        {
            return ProductsDAL.UpdateProduct(connectionString, id, product);
        }

        public bool DeleteProduct(Guid id)
        {
            return ProductsDAL.DeleteProduct(connectionString, id);
        }

        public bool CreateOption(Guid productId, ProductOption option)
        {
            return ProductsDAL.CreateOption(connectionString, productId, option);
        }

        public bool UpdateOption(Guid productId, Guid id, ProductOption option)
        {
            return ProductsDAL.UpdateOption(connectionString, productId, id, option);
        }

        public bool DeleteOption(Guid id)
        {
            return ProductsDAL.DeleteOption(connectionString, id);
        }
    }
}