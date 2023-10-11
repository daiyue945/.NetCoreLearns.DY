using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JWT的提前撤回.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class Demo2Controller : ControllerBase
    {
        private readonly UserManager<MyUser> _userManager;
        public Demo2Controller(UserManager<MyUser> userManager)
        {
            this._userManager = userManager;
        }
        
        [HttpGet]
        public string Test()
        {
            return "333";
        }
    }
}
