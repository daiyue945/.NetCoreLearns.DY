using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace JWT的提前撤回
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
            builder.Services.AddDbContext<MyDbContext>(opt =>
            {
                opt.UseSqlServer("Server=.;Database=EFCoreDemo;Trusted_Connection=True;MultipleActiveResultSets=true;");
            });
            builder.Services.AddDataProtection();
            builder.Services.AddIdentityCore<MyUser>(opt =>
            {
                opt.Password.RequireDigit = false;//是否要求数字
                opt.Password.RequiredLength = 6;//验证密码长度
                opt.Password.RequireLowercase = false;//是否要求小写字母
                opt.Password.RequireNonAlphanumeric = false;//是否要求有非数字非字符
                opt.Password.RequireUppercase = false;//是否要求大写字母
                //密码重置的方式,如果不设定，会出来token超长的情况，这样的token只适合于给邮箱发送链接；设定之后的token验证码是6位数字
                opt.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
                opt.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
                //opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);//默认锁定时长
                //opt.Lockout.MaxFailedAccessAttempts = 5;//最大尝试登录失败次数

            });
            IdentityBuilder identityBuilder = new IdentityBuilder(typeof(MyUser), typeof(MyRole), builder.Services);
            identityBuilder.AddEntityFrameworkStores<MyDbContext>()
                .AddDefaultTokenProviders()
                .AddUserManager<UserManager<MyUser>>()
                .AddRoleManager<RoleManager<MyRole>>();


            builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWT"));
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    var jwtopt = builder.Configuration.GetSection("JWT").Get<JWTSettings>();
                    byte[] keyBytes = Encoding.UTF8.GetBytes(jwtopt.SecKey);
                    var secKey = new SymmetricSecurityKey(keyBytes);
                    opt.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = secKey
                    };
                });
            builder.Services.AddSwaggerGen(s =>
            {
                var scheme = new OpenApiSecurityScheme()
                {
                    Description = "Authorization header.\r\nExample:'Bearer 12345qqwer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Authorization"
                    },
                    Scheme = "oauth2",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                };
                s.AddSecurityDefinition("Authorization", scheme);
                var requirement = new OpenApiSecurityRequirement();
                requirement[scheme] = new List<string>();
                s.AddSecurityRequirement(requirement);
            });
            builder.Services.Configure<MvcOptions>(opt =>
            {
                opt.Filters.Add<JWTVersionCheckFilter>();
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}