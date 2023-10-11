using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SQLServer乐观并发控制
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("请输入您的名称：");
            string name = Console.ReadLine();
            using (MyDbContext ctx = new MyDbContext())
            {
                var house = ctx.Houses.SingleOrDefault(h => h.Id == 1);
                if (!string.IsNullOrEmpty(house.Owner))
                {
                    if (name == house.Owner)
                    {
                        Console.WriteLine($"该房间已经被您抢购，无需重复抢购");
                    }
                    else
                        Console.WriteLine($"房子已经被{house.Owner}抢购");
                    Console.ReadKey();
                    return;
                }
                house.Owner = name;
                Thread.Sleep(5000);
                try
                {
                    Console.WriteLine("恭喜你抢到了");
                    await ctx.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var entry = ex.Entries.First();
                    var dbValues = await entry.GetDatabaseValuesAsync();
                    var newOwner = dbValues.GetValue<string>(nameof(House.Owner));
                    Console.WriteLine($"并发访问冲突,该房间已经被{newOwner}抢购");
                }
                Console.ReadKey();
            }
        }
    }
}
