using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserMgr.Domain;
using UserMgr.Infrastracture;

namespace UserMgr.WebAPI
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
                opt.Filters.Add<UintOfWorkFilter>();
            });
            builder.Services.AddDbContext<UserDbContext>(opt =>
            {
                opt.UseSqlServer("Server=.;Database=EFCoreDDD;Trusted_Connection=True;MultipleActiveResultSets=true;");
            });
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
            });
            builder.Services.AddScoped<UserDomainService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ISmsCodeSender,MockSmsCodeSender>();//应用层进行服务的拼装
            
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