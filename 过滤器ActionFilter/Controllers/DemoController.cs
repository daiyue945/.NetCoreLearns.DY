using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace 过滤器ActionFilter.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly MyDbContext myDbContext;
        public DemoController(MyDbContext myDbContext)
        {
            this.myDbContext = myDbContext;
        }
        [HttpPost]
        [NotTransataion]
        //不需要控制
        public string Test1()
        {
            myDbContext.Books.Add(new Book { Name = "he", Price = 1 });
            myDbContext.SaveChanges();
            //跟数据库表字段长度冲突一定会报错，所以提交成功
            myDbContext.Persons.Add(new Person { Name = "xixixixixi", Age = 10 });
            myDbContext.SaveChanges();

            return "Ok";


        }
        [HttpPost]
        [NotTransataion]
        //同步手动控制
        public string Test2()
        {
            //TransactionScope里面的所有数据都成功才提交成功，否则全部回滚
            using (TransactionScope tx = new TransactionScope())
            {
                myDbContext.Books.Add(new Book { Name = "he", Price = 1 });
                myDbContext.SaveChanges();
                //跟数据库表字段长度冲突一定会报错，所以提交成功
                myDbContext.Persons.Add(new Person { Name = "xixixixixi", Age = 10 });
                myDbContext.SaveChanges();
                tx.Complete();
                return "Ok";
            }

        }
        [HttpPost]
        [NotTransataion]
        //异步手动控制
        public async Task<string> Test3Async()
        {
            //TransactionScope里面的所有数据都成功才提交成功，否则全部回滚
            //异步代码需要改参数：TransactionScopeAsyncFlowOption.Enabled
            using (TransactionScope tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                myDbContext.Books.Add(new Book { Name = "he", Price = 1 });
                await myDbContext.SaveChangesAsync();
                //跟数据库表字段长度冲突一定会报错，所以提交成功
                myDbContext.Persons.Add(new Person { Name = "xi", Age = 10 });
                await myDbContext.SaveChangesAsync();
                tx.Complete();
                return "Ok";
            }

        }

        [HttpPost]
        //异步需要自动控制
        public async Task<string> Test4Async()
        {

            myDbContext.Books.Add(new Book { Name = "he", Price = 1 });
            await myDbContext.SaveChangesAsync();
            //跟数据库表字段长度冲突一定会报错，所以提交成功
            myDbContext.Persons.Add(new Person { Name = "xi好看好看和客户", Age = 10 });
            await myDbContext.SaveChangesAsync();
            return "Ok";


        }
    }
}
