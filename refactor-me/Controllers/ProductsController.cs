using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using refactor_me.Models;

namespace refactor_me.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        [Route]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            ProductsBLL pb = new ProductsBLL();
            List<Product> pl = pb.GetAll();
            if (pl == null || pl.Count <= 0)
            {
                return NotFound();
            }

            return Ok(pl);
        }

        [Route]
        [HttpGet]
        public IHttpActionResult SearchByName(string name)
        {
            ProductsBLL pb = new ProductsBLL();
            Product product = pb.GetProduct(name);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult GetProduct(Guid id)
        {
            ProductsBLL pb = new ProductsBLL();
            Product product = pb.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [Route]
        [HttpPost]
        public void Create(Product product)
        {
            ProductsBLL pb = new ProductsBLL();
            if (!pb.CreateProduct(product))
            {
                StatusCode(HttpStatusCode.BadRequest);
            }
            else
            {
                StatusCode(HttpStatusCode.OK);
            }
        }

        [Route("{id}")]
        [HttpPut]
        public void Update(Guid id, Product product)
        {
            ProductsBLL pb = new ProductsBLL();
            if (!pb.UpdateProduct(id, product))
            {
                StatusCode(HttpStatusCode.NotFound);
            }
            else
            {
                StatusCode(HttpStatusCode.OK);
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public void Delete(Guid id)
        {
            ProductsBLL pb = new ProductsBLL();
            if (!pb.DeleteProduct(id))
            {
                StatusCode(HttpStatusCode.NotFound);
            }
            else
            {
                StatusCode(HttpStatusCode.OK);
            }
        }

        [Route("{productId}/options")]
        [HttpGet]
        public IHttpActionResult GetOptions(Guid productId)
        {
            ProductsBLL pb = new ProductsBLL();
            List<ProductOption> pl = pb.GetProductOptions(productId);
            if (pl == null || pl.Count <= 0)
            {
                return NotFound();
            }

            return Ok(pl);
        }

        [Route("{productId}/options/{id}")]
        [HttpGet]
        public IHttpActionResult GetOption(Guid productId, Guid id)
        {
            ProductsBLL pb = new ProductsBLL();
            ProductOption productoption = pb.GetProductOption(productId, id);
            if (productoption == null)
            {
                return NotFound();
            }

            return Ok(productoption);
        }

        [Route("{productId}/options")]
        [HttpPost]
        public void CreateOption(Guid productId, ProductOption option)
        {
            ProductsBLL pb = new ProductsBLL();
            Product product = pb.GetProduct(productId);
            if (product != null)
            {
                if (pb.CreateOption(productId, option))
                {
                    StatusCode(HttpStatusCode.OK);
                }
                else
                {
                    StatusCode(HttpStatusCode.BadRequest);
                }
            }
            else
            {
                StatusCode(HttpStatusCode.NotFound);
            }
        }

        [Route("{productId}/options/{id}")]
        [HttpPut]
        public void UpdateOption(Guid productId, Guid id, ProductOption option)
        {
            ProductsBLL pb = new ProductsBLL();
            ProductOption productoption = pb.GetProductOption(productId, id);
            if (productoption != null)
            {
                if (pb.UpdateOption(productId, id, option))
                {
                    StatusCode(HttpStatusCode.OK);
                }
                else
                {
                    StatusCode(HttpStatusCode.BadRequest);
                }
            }
            else
            {
                StatusCode(HttpStatusCode.NotFound);
            }
        }

        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public void DeleteOption(Guid productId, Guid id)
        {
            ProductsBLL pb = new ProductsBLL();
            ProductOption productoption = pb.GetProductOption(productId, id);
            if (productoption != null)
            {
                if (pb.DeleteOption(id))
                {
                    StatusCode(HttpStatusCode.OK);
                }
                else
                {
                    StatusCode(HttpStatusCode.BadRequest);
                }
            }
            else
            {
                StatusCode(HttpStatusCode.NotFound);
            }
        }
    }
}
