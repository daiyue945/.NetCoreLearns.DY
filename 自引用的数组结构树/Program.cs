
using System;
using System.Linq;
using System.Threading.Tasks;

namespace 自引用的数组结构树
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //OrgUnit orgRoot = new OrgUnit { Name = "全球总部" };

            ////可以给子节点设置父节点，也可以给父节点增加子节点
            //OrgUnit orgAsia = new OrgUnit { Name = "亚太区总部" };
            //orgRoot.Children.Add(orgAsia);
            //orgAsia.Parent = orgRoot;
            //OrgUnit orgAmerica = new OrgUnit { Name = "美洲总部" };
            //orgAmerica.Parent = orgRoot;
            //orgRoot.Children.Add(orgAmerica);

            //OrgUnit orgUSA = new OrgUnit { Name = "美国分部" };
            //orgUSA.Parent = orgAmerica;
            //orgAmerica.Children.Add(orgUSA);
            //OrgUnit orgCan = new OrgUnit { Name = "加拿大分部" };
            //orgCan.Parent = orgAmerica;
            //orgAmerica.Children.Add(orgCan);

            //OrgUnit orgChina = new OrgUnit { Name = "中国分部" };
            //orgChina.Parent = orgAsia;
            //orgAsia.Children.Add(orgChina);
            //OrgUnit orgSg = new OrgUnit { Name = "新加坡分部" };
            //orgSg.Parent = orgAsia;
            //orgAsia.Children.Add(orgSg);

            //using (MyDbContext ctx=new MyDbContext())
            //{
            //    ctx.OrgUnits.Add(orgRoot);
            //    await ctx.SaveChangesAsync();
            //}


            //OrgUnit orgRoot = new OrgUnit { Name = "全球总部" };

            //OrgUnit orgAsia = new OrgUnit { Name = "亚太区总部" };
            ////orgRoot.Children.Add(orgAsia);
            //orgAsia.Parent = orgRoot;
            //OrgUnit orgAmerica = new OrgUnit { Name = "美洲总部" };
            //orgAmerica.Parent = orgRoot;
            ////orgRoot.Children.Add(orgAmerica);

            //OrgUnit orgUSA = new OrgUnit { Name = "美国分部" };
            //orgUSA.Parent = orgAmerica;
            ////orgAmerica.Children.Add(orgUSA);
            //OrgUnit orgCan = new OrgUnit { Name = "加拿大分部" };
            //orgCan.Parent = orgAmerica;
            ////orgAmerica.Children.Add(orgCan);

            //OrgUnit orgChina = new OrgUnit { Name = "中国分部" };
            //orgChina.Parent = orgAsia;
            ////orgAsia.Children.Add(orgChina);
            //OrgUnit orgSg = new OrgUnit { Name = "新加坡分部" };
            //orgSg.Parent = orgAsia;
            ////orgAsia.Children.Add(orgSg);

            //using (MyDbContext ctx = new MyDbContext())
            //{
            //    ctx.OrgUnits.Add(orgRoot);
            //    ctx.OrgUnits.Add(orgAsia);
            //    ctx.OrgUnits.Add(orgAmerica);
            //    ctx.OrgUnits.Add(orgUSA);
            //    ctx.OrgUnits.Add(orgCan);
            //    ctx.OrgUnits.Add(orgChina);
            //    ctx.OrgUnits.Add(orgSg);
            //    await ctx.SaveChangesAsync();
            //}
            using (MyDbContext ctx=new MyDbContext())
            {
               var p= ctx.OrgUnits.Single(t => t.Parent == null);
                Console.WriteLine(p.Name);
                PrintChildren(1, ctx, p); 
            }
        }
        /// <summary>
        /// 缩进打印所有的子节点
        /// </summary>
        /// <param name="identLevel"></param>
        /// <param name="ctx"></param>
        /// <param name="parent"></param>
        static void PrintChildren(int identLevel, MyDbContext ctx, OrgUnit parent)
        {
            var children = ctx.OrgUnits.Where(t => t.Parent == parent);
            foreach (var child in children)
            {
                Console.WriteLine(new String('\t', identLevel) + child.Name);
                PrintChildren(identLevel + 1,ctx, child);
            }
        }
    }
}
