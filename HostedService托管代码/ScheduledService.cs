using Microsoft.EntityFrameworkCore;

namespace HostedService托管代码
{
    public class ScheduledService : BackgroundService
    {
        private readonly IServiceScope serviceScope;
        public ScheduledService(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScope = serviceScopeFactory.CreateScope();
        }
        public override void Dispose()
        {
            this.serviceScope.Dispose();
            base.Dispose();
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<MyDbContext>();
                while (!stoppingToken.IsCancellationRequested)//后台服务是否关闭
                {
                    //每隔5秒钟做一次数据导出
                    long c = await dbContext.Users.LongCountAsync();
                    await File.WriteAllTextAsync("d:/2.txt", c.ToString());
                    await Task.Delay(5000);
                    await Console.Out.WriteLineAsync($"导出成功,{DateTime.Now}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
    }
}

