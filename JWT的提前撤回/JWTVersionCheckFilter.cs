using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace JWT的提前撤回
{
    public class JWTVersionCheckFilter : IAsyncActionFilter
    {
        private readonly UserManager<MyUser> _userManager;
        public JWTVersionCheckFilter(UserManager<MyUser> _userManager)
        {
            this._userManager = _userManager;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ControllerActionDescriptor? actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (actionDescriptor == null)
            {
                await next();
                return;
            }
            bool isSign = actionDescriptor.MethodInfo.GetCustomAttributes(typeof(NotCheckJWTVersionAttribute), true).Any();
            if (isSign)
            {
                await next();
                return;
            }
            var claimJWTVer = context.HttpContext.User.FindFirst("JWTVersion");
            if (claimJWTVer == null)
            {
                context.Result = new ObjectResult("payload中没有JWTVersion") { StatusCode = 400 };
                return;
            }
            long jwtverFromClient = Convert.ToInt64(claimJWTVer.Value);
            //获取claim中的userid
            var ClaimuserId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //查询数据库中当前的JWTVersion值
            //不用每次都查数据库，否则性能太低，可以用缓存优化。
            var user = await _userManager.FindByIdAsync(ClaimuserId);
            if (user == null)
            {
                context.Result = new ObjectResult("当前用户不存在") { StatusCode = 400 };
                return;
            }
            //如果服务器jwt编号大于客户端jwt中的编号，说明客户端jwt已经失效
            if (user.JWTVersion > jwtverFromClient)
            {
                context.Result = new ObjectResult("客户端jwt过时") { StatusCode = 400 };
                return;
            }
            await next();
        }
    }
}
