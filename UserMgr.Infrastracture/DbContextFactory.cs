using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMgr.Infrastracture
{
    internal class DbContextFactory : IDesignTimeDbContextFactory<UserDbContext>
    {
        public UserDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<UserDbContext> builder = new DbContextOptionsBuilder<UserDbContext>();
            builder.UseSqlServer("Server=.;Database=EFCoreDDD;Trusted_Connection=True;MultipleActiveResultSets=true;");

            return new UserDbContext(builder.Options);
        }
    }
}
