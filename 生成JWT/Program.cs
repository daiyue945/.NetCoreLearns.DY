
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace 生成JWT
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("PassPort", "WDCSJFALS423423SD"));
            claims.Add(new Claim("ID", "234234234234"));
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            claims.Add(new Claim(ClaimTypes.HomePhone, "15323422323"));

            string key = "23423sefwefsddf2!@#$@#$@#$%&^%sdfsdfsdf";//签名的Key
            DateTime expires = DateTime.Now.AddDays(1);//过期时间
            byte[] secBtytes = Encoding.UTF8.GetBytes(key);
            var secKey = new SymmetricSecurityKey(secBtytes);
            var credentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(claims: claims, expires: expires, signingCredentials: credentials);
            string jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            Console.WriteLine(jwt);
        }
    }
}