using Microsoft.AspNetCore.Mvc.Filters;

namespace 过滤器ActionFilter
{
    public class MyActionFilter1 : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Console.WriteLine("MyActionFilter1前代码");
            ActionExecutedContext cr = await next();
            if (cr.Exception != null)
            {
                await Console.Out.WriteLineAsync("MyActionFilter1发生异常");
            }
            else
                await Console.Out.WriteLineAsync("MyActionFilter1执行成功");

        }
    }
}
