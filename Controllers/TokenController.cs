using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductCoreAPI.DBContext;
using ProductCoreAPI.Models;
using ProductCoreAPI.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace ProductCoreAPI.Controllers
{
    [Route("api/token")]
    [ApiController]
    [AllowAnonymous]
    public class TokenController : ControllerBase
    {
        private UserContext _ctx;
        public TokenController(UserContext ctx)
        {
            _ctx = ctx;
        }
        [HttpPost]
        public IActionResult Create([FromBody]User _objUser)
        {          
            if (_objUser == null)
            {
                return BadRequest();
            }
            var data = _ctx.tblUsers.Where(x => x.Username == _objUser.Username && x.Password == _objUser.Password).FirstOrDefault<User>();
            if (data != null)
            {
                var token = new JwtTokenBuilder()
                                .AddSecurityKey(JwtSecurityKey.Create("ProductCoreAPI-secret-key"))
                                .AddSubject("Test")
                                .AddIssuer("ProductCoreAPI.Bearer")
                                .AddAudience("ProductCoreAPI.Bearer")
                                .AddClaim("UserId", _objUser.ID.ToString())
                                .AddExpiry(30)
                                .Build();
                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
        }        
    }
    
}
