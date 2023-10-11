using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 多层项目EFCoreBooks
{
    public class MyDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        //DbContext中不配置数据库连接，而是为DbContext增加一个DbContextOptions类型的构造函数
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            
        }
        //设置该项目为启动项目，在该项目下做数据库迁移
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
