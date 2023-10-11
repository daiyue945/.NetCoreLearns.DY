using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 多层项目EFCoreBooks
{
    internal class MyDbContextDesignFactory : IDesignTimeDbContextFactory<MyDbContext>
    {
        //在开发时(Add-Migration、Update-Database等)运行，生产环境不会用到这个类。
        public MyDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<MyDbContext> optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            //如果不在乎数据库连接字符串被上传Git，就是可以把链接字符串直接写死；
            //如果在乎，那么CreateDbContext里面很难读取到vs中通过简单放方法设置的环境变量，所以必须把连接字符串配置到Windows的正式环境变量中，再读取。
            string connStr = "Data Source=.;Initial Catalog=EFCoreDemo;Integrated Security=SSPI";
            optionsBuilder.UseSqlServer(connStr);
            MyDbContext ctx = new MyDbContext(optionsBuilder.Options);
            return ctx;
        }
    }
}
