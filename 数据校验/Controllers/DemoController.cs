using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace 数据校验.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly UserManager<MyUser> userManager;
        public DemoController(UserManager<MyUser> userManager)
        {
            this.userManager = userManager;
        }
        [HttpPost]
        public async Task<ActionResult> AddNew(AddNewUserRequest request)
        {
            MyUser user = new MyUser() { UserName = request.UserName, Email = request.Email };
            await userManager.CreateAsync(user, request.Password).CheckAsync();
            return Ok(user);
        }
    }
}
