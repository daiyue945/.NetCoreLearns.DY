namespace SignalRAPI
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
            builder.Services.AddSignalR();

            string[] urls = new[] { "http://localhost:3000" };
            builder.Services.AddCors(options =>options.AddDefaultPolicy(builder =>builder.WithOrigins(urls).AllowAnyMethod().AllowAnyHeader().AllowCredentials()));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapHub<MyHub>("/MyHub");
            app.MapControllers();

            app.Run();
        }
    }
}