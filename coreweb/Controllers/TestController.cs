using Microsoft.AspNetCore.Mvc;
using WebCore.Models;

namespace WebCore.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Demo1()
        {
            //return View();
            Person p1 = new Person("zhangsan3", true,DateTime.Now);
            return View(p1);
        }
    }
}
