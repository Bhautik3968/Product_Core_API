using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCoreAPI.DBContext;
using ProductCoreAPI.Models;
using ProductCoreAPI.Helpers;
namespace ProductCoreAPI.Controllers
{
    [Route("api/Product")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private ProductContext _ctx;
        public ProductController(ProductContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_ctx.Product.ToList());
        }

        [HttpGet("{id}", Name = "GetProductByID")]
        public IActionResult GetById(int id)
        {
            var item = _ctx.Product.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Add(Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }          
            _ctx.Product.Add(product);
            _ctx.SaveChanges();
            return CreatedAtRoute("GetProductByID", new { id = product.ID }, product);
        }

        [HttpPut]
        public IActionResult Update(Product product)
        {
            if (product == null || product.ID==0)
            {
                return BadRequest();
            }            
            var item = _ctx.Product.Find(product.ID);
            if (item == null)
            {
                return NotFound();
            }
            item.Name = product.Name;
            item.Category = product.Category;
            item.Price = product.Price;
            item.Quantity = product.Quantity;
            item.Image = product.Image;
            _ctx.Product.Update(item);
            _ctx.SaveChanges();
            //return NoContent();
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _ctx.Product.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            _ctx.Product.Remove(item);
            _ctx.SaveChanges();
            //return NoContent();
            return Ok(id);
        }
    }
}
