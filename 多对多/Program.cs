using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace 多对多
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                //Student s1 = new Student { Name = "张三" };
                //Student s2 = new Student { Name = "李四" };
                //Student s3 = new Student { Name = "王五" };

                //Teacher t1 = new Teacher { Name = "Tom" };
                //Teacher t2 = new Teacher { Name = "Jerry" };
                //Teacher t3 = new Teacher { Name = "Emma" };

                //s1.Teachers.Add(t1);
                //s1.Teachers.Add(t2);

                //s2.Teachers.Add(t2);
                //s2.Teachers.Add(t3);

                //s3.Teachers.Add(t1);
                //s3.Teachers.Add(t2);
                //s3.Teachers.Add(t3);

                //ctx.Students.Add(s1);
                //ctx.Students.Add(s2);
                //ctx.Students.Add(s3);

                //ctx.Teachers.Add(t1);
                //ctx.Teachers.Add(t2);
                //ctx.Teachers.Add(t3);

                //await ctx.SaveChangesAsync();

                var teachers=ctx.Teachers.Include(t => t.Students);
                foreach (var t in teachers)
                {
                    Console.WriteLine($"老师姓名：{t.Name}");
                    foreach (var s in t.Students)
                    {
                        Console.WriteLine($"\t学生姓名：{s.Name}"); 
                    }
                }
            }
        }
    }
}

