using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CRUD.Data;
using CRUD.Models;

namespace CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        ProductsDbContext productsDbContext;

        public ProductsController(ProductsDbContext _productsDbContext)
        {
            productsDbContext = _productsDbContext;
        }

        // GET: api/Products
/*        [HttpGet]
        public IEnumerable<Product> Get(string sortPrice)
        {
            IQueryable<Product> products;
            switch (sortPrice)
            {
                case "desc":
                    products = productsDbContext.Products.OrderByDescending(p => p.ProductPrice);
                    break;
                case "asc":
                    products = productsDbContext.Products.OrderBy(p => p.ProductPrice);
                    break;
                default:
                    products = productsDbContext.Products;
                    break;
            }
            return products;
        }*/

        [HttpGet]
        public IEnumerable<Product> Get(string searchProduct)
        {
            var products = productsDbContext.Products.Where(p => p.ProductName.StartsWith(searchProduct));
            return products;
        }

        // GET: api/Products/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
           var product = productsDbContext.Products.SingleOrDefault(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound("No Records found");
            }
            return Ok(product);
        }

        // POST: api/Products
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            productsDbContext.Products.Add(product);
            productsDbContext.SaveChanges(true);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product product)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(id!=product.ProductID)
            {
                return BadRequest();
            }
            try
            {
            productsDbContext.Products.Update(product);
            productsDbContext.SaveChanges(true);
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
                return NotFound("Given Id does not exist");
            }

            
            return Ok("Product Updated");
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = productsDbContext.Products.SingleOrDefault(m => m.ProductID == id);
            if(product == null)
            {
                return NotFound("No Record Found");
            }
            productsDbContext.Products.Remove(product);
            productsDbContext.SaveChanges(true);
            return Ok("Post Deleted");
        }
    }
}
