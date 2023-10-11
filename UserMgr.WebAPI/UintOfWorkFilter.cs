using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace UserMgr.WebAPI
{
    public class UintOfWorkFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var result = await next();//执行Action方法
            if(result.Exception != null)
            {
                return;//发生异常没执行成功，返回
            }
            var  actionDescriptor=context.ActionDescriptor as ControllerActionDescriptor;
            if(actionDescriptor == null)
            {
                return;
            }
            var uintAttr=actionDescriptor.MethodInfo.GetCustomAttribute<UintOfWorkAttribute>();//看方法是否有标注

            if(uintAttr==null)
            {
                return;
            }
            foreach(var dbContextType in uintAttr.DbContextTypes)
            {
                var dbCtx=context.HttpContext.RequestServices.GetService(dbContextType) as DbContext;//管DI要DbContext实例
                if (dbCtx!=null)
                {
                    await dbCtx.SaveChangesAsync();
                }

            }

        }
    }
}
