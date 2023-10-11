using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (MyDbContext context = new MyDbContext())
            {
                var p = context.Persons.Where(p => p.BirthDay.Year > 1990);
                foreach (var item in p)
                {
                    Console.WriteLine(item.Name);
                }

                //Book b1 = new Book { AuthorName = "张三", TiTle = "C#基础", Price =56.8, PubTime = new DateTime(2017,4,5) };
                //Book b2 = new Book { AuthorName = "李四", TiTle = "Sql语句", Price =30, PubTime = new DateTime(2020,3,16) };
                //Book b3 = new Book { AuthorName = "张三", TiTle = "社会主义", Price =24, PubTime = new DateTime(2015,6,10) };
                //Book b4 = new Book { AuthorName = "王五", TiTle = "糖", Price =245, PubTime = new DateTime(2020,9,29) };
                //Book b5 = new Book { AuthorName = "李四", TiTle = "三体", Price =59, PubTime = new DateTime(2012,6,25) };
                //context.Books.Add(b1);
                //context.Books.Add(b2);
                //context.Books.Add(b3);
                //context.Books.Add(b4);
                //context.Books.Add(b5);
                //await context.SaveChangesAsync();
                //var books = context.Books.Where(b => b.Price > 80);
                //foreach (var bs in books)
                //{
                //    Console.WriteLine(bs.TiTle);
                //}

                //var b = context.Books.Single(b => b.TiTle == "糖");
                //b.AuthorName = "dd4y";

                // await context.SaveChangesAsync();

                //var books2 = context.Books.Where(b => b.Price > 10);
                //Console.WriteLine(books2.Count());

                //foreach (var b2 in books2)
                //{
                //    b2.Price = b2.Price + 1;
                //}
                //await context.SaveChangesAsync();


                //var p = context.Persons.Where(t => t.Name.Contains("d"));
                //string sql=p.ToQueryString();
                //Console.WriteLine($"SQL语句：{ sql}");
                //foreach (var item in p)
                //{
                //    Console.WriteLine(item.Name);
                //}
                
            }       

                 
        }
    }
}
