using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserMgr.Domain;
using UserMgr.Infrastracture;

namespace UserMgr.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserDomainService _userDomainService;
        public LoginController(UserDomainService userDomainService)
        {
            this._userDomainService = userDomainService;
        }

        [HttpPost]
        [UintOfWork(typeof(UserDbContext))]//因为CheckPassWord中可能有修改数据的操作
        public async Task<IActionResult> LoginByPhoneAndPassword(LoginByPhoneAndPasswordRequest req)
        {
            if (req.PassWord.Length <= 3)
            {
                return BadRequest("密码长度必须大于3");
            }
            var checkResult = await _userDomainService.CheckPassWord(req.phoneNumber, req.PassWord);
            switch (checkResult)
            {   
                case UserAccessResult.Ok:
                    return Ok("登录成功");
                case UserAccessResult.PhoneNumberNotFound:
                    return BadRequest("登录失败");
                case UserAccessResult.LockOut:
                    return BadRequest("账户已锁定");
                case UserAccessResult.NoPassWord:
                    return BadRequest("登录失败");
                case UserAccessResult.PassWordError:
                    return BadRequest("登录失败");
                default:
                    throw new ApplicationException($"未知值{checkResult}");
            }
        }
    }
}
