using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace 过滤器Filter
{
    public class MyExceptionFilter : IAsyncExceptionFilter
    {
        private readonly IWebHostEnvironment hostEnvironment;
        public MyExceptionFilter(IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }
        public Task OnExceptionAsync(ExceptionContext context)
        {
            //context.Exception代表异常信息对象
            //如果给context.ExceptionHandled赋值为true，则其他ExecptionFilter不会再执行，表示所有的异常已经处理完成
            //context.Result的值会被输出给客户端
            string message;
            if (hostEnvironment.IsDevelopment())
            {
                message = context.Exception.ToString();//如果是开发环境，则返回异常原本信息
            }
            else
            {
                message = "服务器端发生未处理异常";
            }
            ObjectResult objectResult = new ObjectResult(new { code = 500, msg = message });
            context.Result = objectResult;
            context.ExceptionHandled = true;//如果只记录，不处理则不需要设置为True
            return Task.CompletedTask;
        }
    }
}
