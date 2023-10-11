using Microsoft.AspNetCore.Builder;

namespace 中间件
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            //app.MapGet("/", () => "Hello World!");
            app.Map("/test", async appbuilder =>
            {
                appbuilder.UseMiddleware<CheckMiddleware>();//校验的中间件放在最前面
                appbuilder.Use(async (context, next) =>
                {
                    context.Response.ContentType = "text/html";
                    await context.Response.WriteAsync("1 start<br/>");
                    await next.Invoke();
                    await context.Response.WriteAsync("1 end<br/>");
                });
                appbuilder.Use(async (context, next) =>
               {

                   await context.Response.WriteAsync("2 start<br/>");
                   await next.Invoke();
                   await context.Response.WriteAsync("2 end<br/>");
               });
                appbuilder.UseMiddleware<TestMiddleware>();
                
                appbuilder.Run(async context =>
                {
                    await context.Response.WriteAsync("Run<br/>");
                    dynamic? obj = context.Items["PasswordJson"];
                    if (obj!=null)
                    {
                        await context.Response.WriteAsync($"obj：{obj}<br/>");
                    }
                });
            });

            app.Run();
        }
    }
}