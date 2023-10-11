using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace 综合配置集成.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IOptions<SmtpSettings> options;
        private readonly IConnectionMultiplexer connectionMultiplexer;
        public TestController(IOptions<SmtpSettings> options, IConnectionMultiplexer connectionMultiplexer)
        {
            this.options = options;
            this.connectionMultiplexer = connectionMultiplexer;
        }
        [HttpGet]
        public string? Demo1()
        {
            var ping = connectionMultiplexer.GetDatabase(0).Ping();
            var optp = options.Value.Password;
            var optu = options.Value.UserName;
            return $"{optp}|{optu}|{ping}";
        }
    }
}
