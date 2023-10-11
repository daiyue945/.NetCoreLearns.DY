using Microsoft.AspNetCore.Identity;

namespace Identity框架
{
    public class MyUser : IdentityUser<long>
    {
        public string? Wechat { get; set; }
    }
}
