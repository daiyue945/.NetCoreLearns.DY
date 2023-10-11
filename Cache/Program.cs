using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace Cache
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
            builder.Services.AddMemoryCache();
            builder.Services.AddLogging(x =>
            {
                Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console(new JsonFormatter())//控制台
                //.WriteTo.File($"logs/{DateTime.Now:yyyy-MM-dd}.log")
                .CreateLogger();
                x.AddSerilog();
            });

            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost";//配置redis实例
                options.InstanceName = "test";//避免混乱，配置不冲突的前缀
            });


            var app = builder.Build();
            Console.WriteLine( app.Configuration.GetSection("connstr").Value);

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