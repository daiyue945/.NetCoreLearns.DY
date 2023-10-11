using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MySql悲观并发控制
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Console.WriteLine("请输入您的名称：");
            //string name = Console.ReadLine();
            //using (MyDbContext ctx = new MyDbContext())
            //{
            //    var house = ctx.Houses.SingleOrDefault(h => h.Id == 1);
            //    if (!string.IsNullOrEmpty(house.Owner))
            //    {
            //        if (name == house.Owner)
            //        {
            //            Console.WriteLine($"该房间已经被您抢购，无需重复抢购");
            //        }
            //        else
            //            Console.WriteLine($"房子已经被{house.Owner}抢购");
            //        return;
            //    }
            //    house.Owner = name;
            //    Thread.Sleep(5000);
            //    Console.WriteLine("恭喜你抢到了");
            //    await ctx.SaveChangesAsync();
            //    Console.ReadKey();
            //}

            Console.WriteLine("请输入您的名称：");
            string name = Console.ReadLine();
            using (MyDbContext ctx = new MyDbContext())
            {
                //需要使用事务
                using (var tx = ctx.Database.BeginTransaction())
                {//在事务里面需要用原生SQL语句
                    var house = ctx.Houses.FromSqlInterpolated($"select * from t_houses where id=1 for update").SingleOrDefault();

                    //var house = ctx.Houses.SingleOrDefault(h => h.Id == 1);
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
                    Console.WriteLine("恭喜你抢到了");
                    await ctx.SaveChangesAsync();
                    //结束之后需要提交事务
                    tx.Commit();
                    Console.ReadKey();
                }
            }
        }
    }
}
