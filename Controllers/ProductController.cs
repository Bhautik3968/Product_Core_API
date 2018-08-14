using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCoreAPI.Models;
using ProductCoreAPI.Helpers;
using ProductCoreAPI.Services;
namespace ProductCoreAPI.Controllers
{
    [Route("api/Product")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private IProductCoreAPIRepository _productCoreAPIRepository;
        private IUrlHelper _urlHelper;
        public ProductController(IProductCoreAPIRepository productCoreAPIRepository, IUrlHelper urlHelper)
        {
            _productCoreAPIRepository = productCoreAPIRepository;
            _urlHelper = urlHelper;
        }

        [HttpGet(Name = "GetProducts")]
        public IActionResult GetProducts([FromQuery] ProductResourceParameter productResourceParameter)
        {           
            var products = _productCoreAPIRepository.GetProducts(productResourceParameter);
            var prevPageLink = products.HasPrevious ? CreateProductResourceUri(productResourceParameter, ResourceUriType.PreviousPage) : null;
            var nextPageLink = products.HasNext ? CreateProductResourceUri(productResourceParameter, ResourceUriType.NextPage) : null;
            var paginationMetadata = new
            {
                totalCount = products.TotalCount,
                pageSize = products.PageSize,
                currentPage = products.CurrentPage,
                totalPages = products.TotalPages,
                previousPageLink = prevPageLink,
                nextPageLink = nextPageLink
            };
            Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));
            return Ok(products);
        }

        private string CreateProductResourceUri(ProductResourceParameter productResourceParameter, ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return _urlHelper.Link("GetProducts",
                    new
                    {
                        SearchQuery = productResourceParameter.SearchQuery,
                        name = productResourceParameter.Name,
                        pageNumber = productResourceParameter.pageNumber - 1,
                        pageSize = productResourceParameter.PageSize
                    });
                case ResourceUriType.NextPage:
                    return _urlHelper.Link("GetProducts",
                    new
                    {
                        SearchQuery = productResourceParameter.SearchQuery,
                        name = productResourceParameter.Name,
                        pageNumber = productResourceParameter.pageNumber + 1,
                        pageSize = productResourceParameter.PageSize
                    });
                default:
                    return _urlHelper.Link("GetProducts",
                    new
                    {
                        SearchQuery = productResourceParameter.SearchQuery,
                        name = productResourceParameter.Name,
                        pageNumber = productResourceParameter.pageNumber,
                        pageSize = productResourceParameter.PageSize
                    });
            }

        }

        [HttpGet("{id}", Name = "GetProductByID")]
        public IActionResult GetProduct(int id)
        {
            var item = _productCoreAPIRepository.GetProduct(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            _productCoreAPIRepository.AddProduct(product);
            if (!_productCoreAPIRepository.Save())
            {
                throw new Exception("failed to save product.");
            }
            return CreatedAtRoute("GetProductByID", new { id = product.ID }, product);
        }

        [HttpPut]
        public IActionResult Update([FromBody] Product product)
        {
            if (product == null || product.ID == 0)
            {
                return BadRequest();
            }
            var item = _productCoreAPIRepository.GetProduct(product.ID);
            if (item == null)
            {
                return NotFound();
            }
            item.Name = product.Name;
            item.Category = product.Category;
            item.Price = product.Price;
            item.Quantity = product.Quantity;
            item.Image = product.Image;
            //_productCoreAPIRepository.UpdateProduct(item);
            if (!_productCoreAPIRepository.Save())
            {
                throw new Exception("failed to update product.");
            }
            //return NoContent();
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _productCoreAPIRepository.GetProduct(id);
            if (item == null)
            {
                return NotFound();
            }
            _productCoreAPIRepository.DeleteProduct(item);
            if (!_productCoreAPIRepository.Save())
            {
                throw new Exception("failed to delete product.");
            }
            _productCoreAPIRepository.Save();
            //return NoContent();
            return Ok(id);
        }
    }
}
