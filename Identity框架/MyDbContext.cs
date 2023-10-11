using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity框架
{
    //必须指定<MyUser,MyRole,long>
    public class MyDbContext : IdentityDbContext<MyUser,MyRole,long>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }
    }
}
