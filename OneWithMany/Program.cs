using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using 一对多;
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace OneWithMany
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                //Article a1 = new Article();
                //a1.Title = "看一本书";
                //a1.Message = "学习的进度";

                //Comment c1 = new Comment() { Message = "sadfasdfsadf" };
                //Comment c2 = new Comment() { Message = "不对！" };
                //Comment c3 = new Comment() { Message = "瞎扯" };
                //a1.Comments.Add(c1);
                //a1.Comments.Add(c2);
                //a1.Comments.Add(c3);

                //ctx.Articles.Add(a1);
                //await ctx.SaveChangesAsync();

                //var a=ctx.Articles.Include(c=>c.Comments).SingleOrDefault(a => a.Id == 1);
                //Console.WriteLine($"Id为{a.Id}的标题是：{a.Title}");
                //foreach (var item in a.Comments)
                //{
                //    Console.WriteLine($"当前评论有:{item.Message}");
                //}
                //
                //var c = ctx.Comments.Include(a => a.Articles).Single(a => a.Id == 1);
                //Console.WriteLine($"当前评论{c.Message}的文章标题是{c.Articles.Title}");

                //var c = ctx.Comments.Include(a => a.Articles).Select(s=>new {s.Id,s.Message,s.Articles.Title, ArticleId=s.Articles.Id}).Single(a => a.Id == 1);
                //var c = ctx.Comments.Single(a => a.Id == 1);
                //Console.WriteLine($"当前评论{c.Message}的文章Id是{c.ArticlesId}");

                //var l=ctx.Leaves.FirstOrDefault();
                //if (l!=null)
                //{
                //    Console.WriteLine(l.Remarks);
                //}

                // User u1= new User { Name = "Bob" };
                // User u2 = new User { Name = "Jim" };
                // Leave l1 = new Leave { Remarks = "个人问题",Requester=u1};
                // ctx.Leaves.Add(l1);
                //await ctx.SaveChangesAsync();
                //评论包含微软两个字的文章信息
                //IQueryable<Article>  list=ctx.Articles.Where(a => a.Comments.Any(c => c.Message.Contains("微软")));
                //foreach (var item in list)
                //{
                //    Console.WriteLine($"{item.Title}");
                //}

                //var cs = ctx.Comments.Select(c => new { Id = c.Id, Pre = c.Message.Substring(0, 2) + "..." });
                //foreach (var item in cs)
                //{
                //    Console.WriteLine($"{item.Id}_{item.Pre}");
                //}

                //var cs = ((IEnumerable<Comment>)ctx.Comments).Select(c => new { Id = c.Id, Pre = c.Message.Substring(0, 2) + "..." });
                //foreach (var item in cs)
                //{
                //    Console.WriteLine($"{item.Id}_{item.Pre}");
                //}
                //IQueryable<Article> articles = ctx.Articles;

                //foreach (var item in articles)
                //{
                //    Console.WriteLine(item.Title);
                //}

                //Book b1 = new Book();
                //b1.Name = "《小兔特纳金的故事》";
                //b1.Price = 34.5;

                //Book b2 = new Book();
                //b2.Name = "《拉风丹羽言》";
                //b2.Price = 18;

                //Book b3 = new Book();
                //b3.Name = "《太阳蛋》";
                //b3.Price = 24.5;

                //Book b4 = new Book();
                //b4.Name = "《小房子》";
                //b4.Price = 54.9;





                //    await ctx.AddRangeAsync(b1, b2, b3, b4);

                //ctx.Books.Add(b1);
                //ctx.Books.Add(b2);
                //ctx.Books.Add(b3);
                //ctx.Books.Add(b4);
                //await ctx.SaveChangesAsync();
                //QueryBooks("阳", true, true, 35);

                //await foreach (var item in ctx.Articles.AsAsyncEnumerable())
                //{

                //} 
                //    string name = "hahatest";
                //    FormattableString sql = $@"insert into [dbo].[T_Books](Name,Price,Descript)
                //select {name},Price,Descript from [dbo].[T_Books] where Price>=25";
                // await ctx.Database.ExecuteSqlInterpolatedAsync(sql);
                //var name = "%一本书%";
                ////var queryable = ctx.Articles.FromSqlInterpolated($@"select * from T_Articles where Title like {name} order by NEWID()");
                //var queryable2 = ctx.Articles.FromSqlInterpolated($@"select * from T_Articles where Title like {name}");
                //foreach (var item in queryable2.OrderBy(o=>Guid.NewGuid()).Skip(2).Take(1))
                //{
                //    Console.WriteLine(item.Title);
                //}
                //DbConnection conn = ctx.Database.GetDbConnection();//拿到Context对应的底层的Connection
                //if (conn.State != System.Data.ConnectionState.Open)
                //{
                //    await conn.OpenAsync();
                //}
                //using (var cmd = conn.CreateCommand())
                //{
                //    cmd.CommandText = "select Price,COunt(*) from [dbo].[T_Books] group by Price";
                //    using (var reader=await cmd.ExecuteReaderAsync())
                //    {
                //        while (await reader.ReadAsync())
                //        {
                //            double price=reader.GetDouble(0);
                //            int count = reader.GetInt32(1);
                //            Console.WriteLine($"价格为{price}的数量是{count}");
                //        }
                //    }
                //}

                //var articles =ctx.Articles.AsNoTracking().Where(t => t.Title.Contains("书"));
                //Book book = new Book { Id = 1 };
                ////book.Price = 59;
                ////ctx.Entry(book).Property(p => p.Price).IsModified = true;
                ////await ctx.SaveChangesAsync();


                //ctx.Entry(book).State = EntityState.Deleted;
                //await ctx.SaveChangesAsync();

                foreach (var item in ctx.Books.IgnoreQueryFilters().Where(a=>a.IsDelete))
                {
                    Console.WriteLine(item.Name);
                }

            }

        }

        static void QueryBooks(string searchWords, bool searchAll, bool orderByPrice, double upperPrice)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                IQueryable<Book> books = ctx.Books.Where(a => a.Price <= upperPrice);
                if (searchAll)
                {
                    books = books.Where(a => a.Name.Contains(searchWords) || a.Descript.Contains(searchWords));
                }
                else
                    books = books.Where(a => a.Name.Contains(searchWords));
                if (orderByPrice)
                {
                    books = books.OrderBy(b => b.Price);
                }
                foreach (var item in books)
                {
                    Console.WriteLine($"{item.Name}");
                }
            }
        }
    }
}
