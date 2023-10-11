using Microsoft.EntityFrameworkCore;
using System.Reflection;
using 多层项目EFCoreBooks;

namespace 多层项目API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //初始化连接数据库,适用数据库连接池机制，可能会有冲突导致数据库连接池耗尽，慎用
            //builder.Services.AddDbContextPool<MyDbContext>(opt => {
            //    var str = builder.Configuration.GetSection("ConnStr").Value;
            //    opt.UseSqlServer(str);
            //});
            ////初始化连接数据库，推荐
            //builder.Services.AddDbContext<MyDbContext>(opt =>
            //{
            //    var str = builder.Configuration.GetSection("ConnStr").Value;
            //    opt.UseSqlServer(str);
            //});

            //builder.Services.AddDbContext<MyDbContext2>(opt =>
            //{
            //    var str = builder.Configuration.GetSection("ConnStr").Value;
            //    opt.UseSqlServer(str);
            //});

            //初始化多个同数据库的链接，指定程序集
            var asms = new Assembly[] { Assembly.Load("多层项目EFCoreBooks") };
            builder.Services.AddAllDbContexts(opt =>
            {
                var str = builder.Configuration.GetSection("ConnStr").Value;
                opt.UseSqlServer(str);
            }, asms);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}