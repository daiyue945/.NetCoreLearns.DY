using Microsoft.AspNetCore.Identity;

namespace HostedService托管代码
{
    public class MyUser : IdentityUser<long>
    {
        public string? Wechat { get; set; }
        public long JWTVersion { get; set; }
    }
}
