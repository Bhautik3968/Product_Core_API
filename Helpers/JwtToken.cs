using System;
using System.IdentityModel.Tokens.Jwt;
namespace ProductCoreAPI.Helpers
{
    public sealed class JwtToken
    {
        private JwtSecurityToken token;
        internal JwtToken(JwtSecurityToken token)
        {
            this.token = token;
        }
        public DateTime expires_in => token.ValidTo;
        public string access_token => new JwtSecurityTokenHandler().WriteToken(this.token);
        public string token_type="bearer";
    }
}