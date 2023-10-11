using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 领域事件发布的时机
{
    public class MyDbContext : DbContext
    {
        private IMediator mediator;
        public MyDbContext(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.;Database=EFCoreDDD;Trusted_Connection=True;MultipleActiveResultSets=true;");
            optionsBuilder.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var domainEntites = this.ChangeTracker.Entries<IDomainEvents>()
                .Where(e => e.Entity.GetDomainEvents().Any());//获取所有包含有未发布事件的实体对象

            var domainEvents = domainEntites.SelectMany(e => e.Entity.GetDomainEvents()).ToList();//获取所有待发布消息

            domainEntites.ToList().ForEach(e => { e.Entity.ClearDomainEvents(); });//清除待发布消息

            foreach (var item in domainEvents)
            {
                await mediator.Publish(item);
            }
            //把消息发布放在真正保存之前可以保证领域事件响应代码中的事务操作
            //和base.SaveChangesAsync中的代码在同一个事务中，实现事务强一致性
            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
