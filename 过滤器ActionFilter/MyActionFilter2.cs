using Microsoft.AspNetCore.Mvc.Filters;

namespace 过滤器ActionFilter
{
    public class MyActionFilter2 : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Console.WriteLine("MyActionFilter2前代码");
            ActionExecutedContext cr = await next();
            if (cr.Exception != null)
            {
                await Console.Out.WriteLineAsync("MyActionFilter2发生异常");
            }
            else
                await Console.Out.WriteLineAsync("MyActionFilter2执行成功");

        }
    }
}
