using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity框架.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly UserManager<MyUser> _userManager;
        private readonly RoleManager<MyRole> _roleManager;
        private readonly IWebHostEnvironment _hostEnvironment;
        public DemoController(UserManager<MyUser> userManager,
            RoleManager<MyRole> roleManager,
            IWebHostEnvironment hostEnvironment)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._hostEnvironment = hostEnvironment;
        }
        [HttpPost]
        public async Task<ActionResult<string>> AddRoleAndUser()
        {
            bool IsExistRole = await _roleManager.RoleExistsAsync("Admin");
            if (IsExistRole == false)
            {
                MyRole role = new MyRole() { Name = "admin" };
                var result = await _roleManager.CreateAsync(role);
                if (!result.Succeeded)
                {
                    return BadRequest("CreateAsync Failed");
                }
            }
            MyUser user = await _userManager.FindByNameAsync("dy");
            if (user == null)
            {
                user = new MyUser() { UserName = "dy" };
                var result = await _userManager.CreateAsync(user, "123456");
                if (!result.Succeeded)
                {
                    return BadRequest("_userManager.CreateAsync Failed");
                }
            }
            if (!await _userManager.IsInRoleAsync(user, "admin"))
            {
                var result = await _userManager.AddToRoleAsync(user, "admin");
                if (!result.Succeeded)
                {
                    return BadRequest("_userManager.AddToRoleAsync Failed");
                }
            }
            return "ok";
        }

        [HttpPost]
        public async Task<ActionResult> CheckPwd(CheckPwdRequest request)
        {
            string userName = request.UserName;
            string pwd = request.PassWord;
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                if (_hostEnvironment.IsDevelopment())
                {
                    return BadRequest("用户名不存在");
                }
                else
                {
                    return BadRequest();//更安全    
                }
            }
            if (await _userManager.IsLockedOutAsync(user))
            {
                return BadRequest($"该用户已被锁定,锁定结束时间：{user.LockoutEnd}");
            }
            if (await _userManager.CheckPasswordAsync(user, pwd))
            {
                await _userManager.ResetAccessFailedCountAsync(user);//重置登录失败次数
                return Ok("登录成功");
            }
            else
            {
                await _userManager.AccessFailedAsync(user);//记录登录失败信息
                return BadRequest("用户名密码错误");
            }
        }

        [HttpPost]
        public async Task<ActionResult> SendResetPassWordToken(string UserName)
        {
            var user = await _userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                return BadRequest("用户名不存在");
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            await Console.Out.WriteLineAsync($"验证码是：{token}");
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> ResetPassWord(string userName, string token, string PassWordNew)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return BadRequest("用户名不存在");
            }
            var result = await _userManager.ResetPasswordAsync(user, token, PassWordNew);
            if (result.Succeeded)
            {
                await _userManager.ResetAccessFailedCountAsync(user);
                return Ok("密码重置成功");
            }
            else
            {
                await _userManager.AccessFailedAsync(user);
                return BadRequest("密码重置失败");
            }
        }
    }
}
