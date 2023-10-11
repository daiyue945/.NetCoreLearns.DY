using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 多层项目EFCoreBooks
{
    public class MyDbContext2: DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public MyDbContext2(DbContextOptions<MyDbContext2> options) : base(options)
        {

        }
    }
}
