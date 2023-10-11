using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JWTWebApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class Login2Controller : ControllerBase
    {
        [HttpGet]
        public string Test()
        {
            var claim = this.User.FindFirst(ClaimTypes.Name)!.Value;
            IEnumerable<Claim> roleclaims = this.User.FindAll(ClaimTypes.Role);
            string roleName = string.Join(" ", roleclaims.Select(r => r.Value));
            return $"OK：{claim},{roleName ?? "角色未知"}";
        }

        [HttpGet]
        [AllowAnonymous]
        public string TestAllowAnonymous()
        {
            return "23423";
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public string TestWithRole()
        {
            return "333";
        }
    }
}
