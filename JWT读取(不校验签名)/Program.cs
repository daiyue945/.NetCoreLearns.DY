using System.Text;

namespace JWT读取_不校验签名_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string jwt = Console.ReadLine();
            string[] strs = jwt.Split('.');
            string header = strs[0];
            string payload = strs[1];
            string sig = strs[2];
            Console.WriteLine($"header:{JwtDecode(header)}");
            Console.WriteLine($"payload:{JwtDecode(payload)}");
            Console.WriteLine($"sig:{JwtDecode(sig)}");
        }
        public static string JwtDecode(string s)
        {
            s = s.Replace('-', '+').Replace('_', '/');
            switch (s.Length % 4)
            {
                case 2:
                    s += " ==";
                    break;
                case 3:
                    s += "=";
                    break;
            }
            var bytes = Convert.FromBase64String(s);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}