using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace 过滤器ActionFilter
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

            builder.Services.Configure<MvcOptions>(opt =>
            {
                opt.Filters.Add<RateLimitActionFilter>();//限制10秒钟内多次访问，放在最前面，避免资源浪费
                opt.Filters.Add<MyActionFilter1>();//ActionFilter先进后出，后进先出
                opt.Filters.Add<MyActionFilter2>();
                opt.Filters.Add<TransactionScopeFilter>();
                
            });
            builder.Services.AddMemoryCache();
            builder.Services.AddDbContext<MyDbContext>(opt =>
            {
                string connStr = "Data Source=.;Initial Catalog=EFCoreDemo;Integrated Security=SSPI";
                opt.UseSqlServer(connStr);
            });

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