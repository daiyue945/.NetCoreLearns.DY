using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT读取_校验签名_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string jwt = Console.ReadLine();
            string key = "23423sefwefsddf2!@#$@#$@#$%&^%sdfsdfsdf";//签名的Key
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            TokenValidationParameters valParam = new();
            valParam.IssuerSigningKey = securityKey;
            valParam.ValidateIssuer = false;
            valParam.ValidateAudience = false;
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            ClaimsPrincipal principal = tokenHandler.ValidateToken(jwt, valParam, out SecurityToken securityToken);
            foreach (var claim in principal.Claims)
            {
                Console.WriteLine($"{claim.Type}:{claim.Value}");
            }
        }
    }
}