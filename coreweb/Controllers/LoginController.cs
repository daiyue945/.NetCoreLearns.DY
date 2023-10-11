using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace WebCore.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult loginSubmit(string UserName, string PassWord)
        {
            if (string.IsNullOrEmpty(UserName))
            {
                return View("用户名不能为空");
            }
            if (string.IsNullOrEmpty(PassWord))
            {
                return View("密码不能为空");
            }

            dynamic person = new System.Dynamic.ExpandoObject();
            person.UserName = UserName;
            person.PassWord = PassWord;
            var parmater = JsonConvert.SerializeObject(person);
            var url = "https://localhost:7218/Login/Login";
            var result = new HttpClient().PostAsync(url, parmater).Result;
            return View(result);
        }
    }
}
