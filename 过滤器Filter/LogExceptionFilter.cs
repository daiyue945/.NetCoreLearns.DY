using Microsoft.AspNetCore.Mvc.Filters;

namespace 过滤器Filter
{
    public class LogExceptionFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            return File.AppendAllTextAsync("D:/error.log", context.Exception.ToString());
        }
    }
}
