namespace 中间件
{
    public class TestMiddleware
    {
        private readonly RequestDelegate _next;
        public TestMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await context.Response.WriteAsync("TestMiddleware start<br/>");
            await _next.Invoke(context);
            await context.Response.WriteAsync("TestMiddleware end<br/>");
        }
    }
}
