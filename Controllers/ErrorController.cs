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
    [Route("api/Error")]
    [ApiController]
    [Authorize]
    public class ErrorController : ControllerBase
    {
        private DbErrorContext _ctx;
        public ErrorController(DbErrorContext ctx)
        {
            _ctx = ctx;
        }
         
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_ctx.API_Errors.ToList());
        }

        [HttpGet("{id}", Name = "GetErrorByID")]
        public IActionResult GetById(int id)
        {
            var item = _ctx.API_Errors.Find(id);
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
            _ctx.API_Errors.Add(error);
            _ctx.SaveChanges();
            return CreatedAtRoute("GetErrorByID", new { id = error.ID }, error);
        }       
    }
}
