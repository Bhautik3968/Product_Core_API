using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductCoreAPI.DBContext;
using ProductCoreAPI.Models;
using ProductCoreAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using ProductCoreAPI.Services;
namespace ProductCoreAPI.Controllers
{
    [Route("api/token")]
    [ApiController]
    [AllowAnonymous]
    public class TokenController : ControllerBase
    {
        private IProductCoreAPIRepository _productCoreAPIRepository;
        public TokenController(IProductCoreAPIRepository productCoreAPIRepository)
        {
            _productCoreAPIRepository = productCoreAPIRepository;
        }
        [HttpPost]
        public IActionResult Create([FromBody]User _objUser)
        {
            if (_objUser == null)
            {
                return BadRequest();
            }
            var data = _productCoreAPIRepository.GetUsers().Where(x => x.Username == _objUser.Username && x.Password == _objUser.Password).FirstOrDefault<User>();
            if (data != null)
            {
                var token = _productCoreAPIRepository.GetToken(_objUser.ID);
                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
        }
    }

}
