using System;

namespace 一对一
{
    class Program
    {
        static void Main(string[] args)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                Order o1 = new Order();
                o1.Name = "ccc";

                Delivery delivery = new Delivery();
                delivery.CompanyName = "ce快递";
                delivery.Name = "cccc1111";
                delivery.Order = o1;

                //ctx.Orders.Add(o1);
                ctx.Deliveries.Add(delivery);
                ctx.SaveChanges();
            }
        }
    }
}
