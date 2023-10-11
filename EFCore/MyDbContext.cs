using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore
{
    class MyDbContext : DbContext
    {

        //private static ILoggerFactory loggerFactory = LoggerFactory.Create(b => b.AddConsole());
        public DbSet<Book> Books { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Cat> Cats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseSqlServer("Server=.;Database=EFCoreDemo;Trusted_Connection=True;MultipleActiveResultSets=true;");
            //optionsBuilder.UseMySql("server=localhost;user=root;password=12345678;database=EFCoreDemo", new MySqlServerVersion(new Version(8,0,31)));
            optionsBuilder.UseNpgsql("Host=127.0.0.1;Database=EFCoreDemo;Username=postgres;Password=12345678");
            //optionsBuilder.UseLoggerFactory(loggerFactory);
            optionsBuilder.LogTo(msg =>
            {
                 if (!msg.Contains("CommandExecuting")) return;
                Console.WriteLine(msg);
            });
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //从当前程序集加载所有的IEntityTypeConfiguration配置
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }

    }
}
