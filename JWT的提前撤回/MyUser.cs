using Microsoft.AspNetCore.Identity;

namespace JWT的提前撤回
{
    public class MyUser : IdentityUser<long>
    {
        public string? Wechat { get; set; }
        public long JWTVersion { get; set; }
    }
}
