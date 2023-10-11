using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace 过滤器ActionFilter
{
    //限制10秒钟内多次访问的过滤器
    public class RateLimitActionFilter : IAsyncActionFilter
    {
        private readonly IMemoryCache memoryCache;
        public RateLimitActionFilter(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            string ip = context.HttpContext.Connection.RemoteIpAddress.ToString();//用户的IP地址
            string cacheKey = $"LasVisitTick_{ip}";
            long? LasVisit = memoryCache.Get<long?>(cacheKey);//最近一次访问时间
            if (LasVisit == null || Environment.TickCount64 - LasVisit > 1000)//没有访问过或者是最近一次访问已经过了1秒了
            {
                memoryCache.Set(cacheKey, Environment.TickCount64, TimeSpan.FromSeconds(10));//避免长期不访问的用户占用缓存
                await next();
            }
            else
            {
                ObjectResult result = new ObjectResult("访问过于频繁") { StatusCode = 429 };
                context.Result = result;
                
            }

        }
    }
}
