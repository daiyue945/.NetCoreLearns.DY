using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace 过滤器Filter.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        [HttpGet]
        public string Test1()
        {
            //不存在文件会报错
            string s = System.IO.File.ReadAllText("f:/1.txt");
            return s;
        }
            
    }
}
