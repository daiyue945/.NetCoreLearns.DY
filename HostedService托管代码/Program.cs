using Microsoft.EntityFrameworkCore;

namespace HostedService托管代码
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

            builder.Services.AddHostedService<HostedService1>();
            builder.Services.AddScoped<TestService>();
            builder.Services.AddHostedService<ScheduledService>();
            builder.Services.AddDbContext<MyDbContext>(opt =>
            {
                opt.UseSqlServer("Server=.;Database=EFCoreDemo;Trusted_Connection=True;MultipleActiveResultSets=true;");
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