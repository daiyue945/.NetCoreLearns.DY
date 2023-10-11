namespace EF_Core充血模型
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var ctx = new MyDbContext();

            /* 插入数据
            User u1 = new User("xm");
            u1.ChangePassword("123");
            ctx.Users.Add(u1);
            ctx.SaveChanges();
            */

            //var user=ctx.Users.First();
            //Console.WriteLine(user.Remark);

            //CommodityEntity commodity = new CommodityEntity();
            //commodity.Name = "触控笔";
            //commodity.currencyName = CurrencyName.NZD;

            //CommodityEntity commodity2 = new CommodityEntity();
            //commodity2.Name = "电脑包";
            //commodity2.currencyName = CurrencyName.USD;

            //ctx.Add(commodity);
            //ctx.Add(commodity2);

            //ctx.SaveChanges();
            //var c = ctx.CommodityEntitys.ToList();

            //foreach (var item in c)
            //{
            //    Console.WriteLine(item.currencyName);

            //}


            //Shop shop1 = new Shop() { Name = "久福居", Location =new Geo(123.11,165.22)};
            //ctx.Add(shop1);
            //ctx.SaveChanges();
            //foreach (var item in ctx.Shops.ToList())
            //{
            //    Console.WriteLine(item.Location);
            //    Console.WriteLine($"{item.Name}-{item.Location.Longitude}:{item.Location.Latitude}");
            //}

            //Blog    blog = new Blog() { 
            //    Title=new MultiLangString("你好","Hello"),
            //    Body=new MultiLangString("这是一篇短文","This is a ...")
            //};
            //Blog blog2 = new Blog()
            //{
            //    Title = new MultiLangString("再见", "Bye"),
            //    Body = new MultiLangString("这篇短文结束", "The End")
            //};

            //ctx.Add(blog);
            //ctx.Add(blog2);
            //ctx.SaveChanges();

            //var b = ctx.Blogs.FirstOrDefault(x => x.Title.Chinese.Contains("你好") && x.Title.English.Contains("hello"));
            //Console.WriteLine($"中文版标题：{b.Title.Chinese}英文版标题：{b.Title.English},内容是:{b.Body.Chinese}--{b.Body.English}");

            Merchan merchan = new Merchan() { Name = "键盘", Price = 100 };

            Order order = new Order();
            order.CreateDateTime = DateTime.Now;
            order.TotalAmount = 1000;

            order.AddDeatil(merchan,10);
            ctx.Add(order);
            ctx.SaveChanges();
        }
    }
}