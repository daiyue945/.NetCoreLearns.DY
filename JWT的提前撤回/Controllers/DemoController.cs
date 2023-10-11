using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT的提前撤回.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly IOptionsSnapshot<JWTSettings> settings;
        private readonly UserManager<MyUser> userManager;
        public DemoController(UserManager<MyUser> userManager, IOptionsSnapshot<JWTSettings> settings)
        {
            this.userManager = userManager;
            this.settings = settings;   
        }
        [HttpPost]
        [NotCheckJWTVersion]
        public async Task<ActionResult<string>> Login(string userName, string password)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return BadRequest("用户名或者密码错误");
            }
            if(await userManager.CheckPasswordAsync(user,password))
            {
                await userManager.ResetAccessFailedCountAsync(user).CheckAsync();
                
                user.JWTVersion++;//!!
                await userManager.UpdateAsync(user);//!!
                
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                claims.Add(new Claim("JWTVersion", user.JWTVersion.ToString()));//!!
                var roles=await userManager.GetRolesAsync(user);
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                string key=settings.Value.SecKey;
                DateTime expire = DateTime.Now.AddSeconds(settings.Value.ExpireSeconds);
                byte[] secBtytes = Encoding.UTF8.GetBytes(key);
                var secKey = new SymmetricSecurityKey(secBtytes);
                var credentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256Signature);
                var tokenDescriptor = new JwtSecurityToken(claims: claims, expires: expire, signingCredentials: credentials);
                string jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
                return jwt;
            }
            else
            {
                await userManager.AccessFailedAsync(user).CheckAsync();
                return BadRequest("用户名或者密码错误");
            }
        }
    }
}
