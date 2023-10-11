using Microsoft.AspNetCore.Identity;

namespace 数据校验
{
    public class MyUser : IdentityUser<long>
    {
        public string? Wechat { get; set; }
    }
}
