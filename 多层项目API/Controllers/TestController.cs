using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using 多层项目EFCoreBooks;

namespace 多层项目API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    { 
        //asp.net core项目中引用EFCore项目，并且通过AddDbContext来注入DbContext以及对DbContext的配置。
        private readonly MyDbContext myDbContext;
        private readonly MyDbContext2 myDbContext2;
        public TestController(MyDbContext myDbContext, MyDbContext2 myDbContext2)
        {
            this.myDbContext = myDbContext;
            this.myDbContext2 = myDbContext2;
        }
        [HttpGet]
        public string Test1()
        {
            int bookNum = myDbContext.Books.Count();
            int PersonNum=myDbContext2.Persons.Count();
            return $"书本数量:{bookNum}，用户数量：{PersonNum}";
        }
    }
}
