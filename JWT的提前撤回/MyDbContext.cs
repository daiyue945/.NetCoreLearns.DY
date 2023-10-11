using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JWT的提前撤回
{
    //必须指定<MyUser,MyRole,long>
    public class MyDbContext : IdentityDbContext<MyUser,MyRole,long>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }
    }
}
