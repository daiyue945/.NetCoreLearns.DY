using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Identity框架
{
    public class DbContextDesignTimeFactory : IDesignTimeDbContextFactory<MyDbContext>
    {
        public MyDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<MyDbContext> builder=new DbContextOptionsBuilder<MyDbContext>();
            builder.UseSqlServer("Server=.;Database=EFCoreDemo;Trusted_Connection=True;MultipleActiveResultSets=true;");
            return new MyDbContext(builder.Options);
        }
    }
}
