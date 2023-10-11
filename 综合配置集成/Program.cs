using Microsoft.AspNetCore.Hosting;
using StackExchange.Redis;
using System.Data.SqlClient;

namespace 综合配置集成
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
            builder.WebHost.ConfigureAppConfiguration((hostCtx, configBinder) =>
            {
                string connStr = builder.Configuration.GetSection("ConnStr").Value;
                configBinder.AddDbConfiguration(() => new SqlConnection(connStr),
                    reloadOnChange: true,
                    reloadInterval: TimeSpan.FromSeconds(2));
            });

            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                string connstr = builder.Configuration.GetValue<string>("Redis");
                //string connstr = builder.Configuration.GetSection("Redis").Value;
                return ConnectionMultiplexer.Connect(connstr);
            });
            builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("Smtp"));

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