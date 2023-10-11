using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace Cache.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMemoryCache memoryCache;
        private readonly ILogger<Book> logger;
        private readonly IDistributedCache distributedCache;
        private readonly IWebHostEnvironment webHostEnvironment;
        public BookController(IMemoryCache memoryCache, ILogger<Book> logger, IDistributedCache distributedCache, IWebHostEnvironment webHostEnvironment)
        {
            this.memoryCache = memoryCache;
            this.logger = logger;
            this.distributedCache = distributedCache;
            this.webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public async Task<ActionResult<Book?>> GetBookById(long id)
        {
            //Book? book = MyDbContext.GetById(id);
            //if (book == null)
            //{
            //    return NotFound($"找不到Id为{id}");
            //}
            //else
            //    return Ok(book);
            logger.LogDebug($"开始执行{id}");
            //GetOrCreateAsync二合一：1、从缓存取数据，2、从数据源去数据，并且返回给调用者并保存到缓存
            Book? book2 = await memoryCache.GetOrCreateAsync("Book" + id, async (e) =>
             {
                 logger.LogDebug($"缓存没有查到数据，从数据库读取{id}");
                 //e.AbsoluteExpirationRelativeToNow=TimeSpan.FromSeconds(30);//设置缓存绝对过期时间30秒
                 //e.SlidingExpiration=TimeSpan.FromSeconds(10);//设置滑动过期时间10秒
                 e.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(Random.Shared.Next(10, 15));
                 Book? b = await MyDbContext.GetByIdAsync(id);
                 logger.LogDebug($"从数据库中查询的结果是{(b == null ? "null" : b)}");
                 return b;
             });
            logger.LogDebug($"结果 {(book2 == null ? "null" : book2)}");
            if (book2 == null)
            {
                return NotFound($"找不到Id为{id}");
            }
            else
                return Ok(book2);
        }
        [HttpGet]
        public async Task<ActionResult<Book?>> GetBookById2(long id)
        {
            Book? book;
            string? s = await distributedCache.GetStringAsync("Book" + id);
            if (s == null)
            {
                await Console.Out.WriteLineAsync($"从数据库中读取");
                book = await MyDbContext.GetByIdAsync(id);
                await distributedCache.SetStringAsync("Book" + id, JsonSerializer.Serialize(book));
            }
            else
            {
                await Console.Out.WriteLineAsync("从redis缓存中读取");
                book = JsonSerializer.Deserialize<Book?>(s);
            }

            if (book == null)
            {
                return NotFound($"找不到Id为{id}");
            }
            else
                return Ok(book);
        }

        [HttpGet]
        public async Task<ActionResult<string>> Gethjbl()
        {
            return webHostEnvironment.EnvironmentName;
        }
    }
}
