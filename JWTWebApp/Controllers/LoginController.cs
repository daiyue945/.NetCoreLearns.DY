using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTWebApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IOptionsSnapshot<JwtSettings> _settings;
        public LoginController(IOptionsSnapshot<JwtSettings> settings)
        {
            this._settings = settings;
        }
        [HttpPost]
        public ActionResult<string> LoginIn(string User, string PassWord)
        {
            if (User == "admin" || PassWord == "123456")
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, "1"));
                claims.Add(new Claim(ClaimTypes.Name, "admin"));
                claims.Add(new Claim(ClaimTypes.Role, "admin"));
                string key = _settings.Value.SecKey;//通过配置读取签名的Key
                DateTime expires = DateTime.Now.AddSeconds(_settings.Value.ExpireSeconds);//通过配置读取过期时间
                byte[] secBtytes = Encoding.UTF8.GetBytes(key);
                var secKey = new SymmetricSecurityKey(secBtytes);
                var credentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256Signature);
                var tokenDescriptor = new JwtSecurityToken(claims: claims, expires: expires, signingCredentials: credentials);
                string jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
                return jwt;
            }
            else
                return BadRequest("用户名密码错误");
        }
    }
}
