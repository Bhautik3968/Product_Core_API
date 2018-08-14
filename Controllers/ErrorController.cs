using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCoreAPI.DBContext;
using ProductCoreAPI.Models;
using ProductCoreAPI.Helpers;
using ProductCoreAPI.Services;
namespace ProductCoreAPI.Controllers
{
    [Route("api/Error")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private IProductCoreAPIRepository _productCoreAPIRepository;
        public ErrorController(IProductCoreAPIRepository productCoreAPIRepository)
        {
            _productCoreAPIRepository = productCoreAPIRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_productCoreAPIRepository.GetErrors());
        }

        [HttpGet("{id}", Name = "GetErrorByID")]
        public IActionResult GetById(int id)
        {
            var item = _productCoreAPIRepository.GetError(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Add(Error error)
        {
            if (error == null)
            {
                return BadRequest();
            }
            _productCoreAPIRepository.AddError(error);
            _productCoreAPIRepository.Save();
            return CreatedAtRoute("GetErrorByID", new { id = error.ID }, error);
        }
    }
}
