using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public Person GetPerson()
        {
            return new Person("阿是两地分居", 45);
        }
        [HttpPost]
        public string SaveNote(SaveNotRequest req)
        {
            System.IO.File.WriteAllText(req.Title + ".txt", req.Content);
            return "OK";
        }
        [HttpGet]
        public int Multis(int i, int j)
        {
            return i + j;
        }

        [HttpGet("{i}/{j}")]
        public int Multi2(int i, int j)
        {
            return i + j;
        }

        [HttpGet("{i}/{j}")]
        public int Multi3(int i, [FromRoute(Name = "j")] int x)
        {
            return i + x;
        }
    }
}
