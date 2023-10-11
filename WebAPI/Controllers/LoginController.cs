using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace WebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public LoginReponse Login(LoginRequest request)
        {
            if (request.UserName == "admin" && request.PassWord == "123456")
            {
                var items = Process.GetProcesses().Select(p =>
                new ProcessInfo(p.Id, p.ProcessName, p.WorkingSet64));
                return new LoginReponse(true, items.ToArray());
            }
            else
            {
                return new LoginReponse(false, null);
            }
        }
    }
    //请求
    public record LoginRequest(string UserName, string PassWord);
    //响应详情
    public record ProcessInfo(long Id, string Name, long WorkingSet);
    //响应
    public record LoginReponse(bool OK, ProcessInfo[]? ProcessInfos);
}
