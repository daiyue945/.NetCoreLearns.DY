using Dynamic.Json;

namespace 中间件
{
    public class CheckMiddleware
    {
        private readonly RequestDelegate _next;
        public CheckMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //https://localhost:7289/test?Password=123 增加Password正确，否则显示401
            var password = context.Request.Query["Password"];
            if (password == "123")
            {
                bool isJson = context.Request.HasJsonContentType();//看请求报文体是否是JSON请求
                if (isJson)
                {
                    var stream = context.Request.BodyReader.AsStream();//获取请求流
                    dynamic? jsonObj = DJson.ParseAsync(stream);//把流反序列化为dynamic
                    context.Items["PasswordJson"]= jsonObj;
                }
                await _next.Invoke(context);
            }
            else
            {
                context.Response.StatusCode = 401;
            }
        }
    }
}
