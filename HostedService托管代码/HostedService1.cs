namespace HostedService托管代码
{
    public class HostedService1 : BackgroundService
    {
        //托管服务是以单例的生命周期注册到依赖服务容器中的，因此不能注入生命周期wield范围或瞬态的服务
        //private readonly TestService _service;
        //public HostedService1(TestService _service)
        //{
        //    this._service = _service;
        //}
        private readonly IServiceScope serviceScope;
        //通过构造方法注入一个IServiceScopeFactory服务，用它来创建一个IServiceScope对象，
        //这样就可以通过IServiceScope来创建短生命周期的服务了
        public HostedService1(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScope = serviceScopeFactory.CreateScope();
        }
        //需要重写Dispose方法，BackgroundService销毁的时候serviceScope也要同时销毁
        public override void Dispose()
        {
            this.serviceScope.Dispose();
            base.Dispose();
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var testservice= serviceScope.ServiceProvider.GetRequiredService<TestService>();
                //await Console.Out.WriteLineAsync($"HostedService1启动,{_service.Add(1,2)}");
                await Console.Out.WriteLineAsync($"HostedService1启动,{testservice.Add(1, 2)}");
                await Task.Delay(3000);//不能用sleep
                string txt = await File.ReadAllTextAsync("D:/1.txt");
                await Console.Out.WriteLineAsync("文件读取完成");
                //程序会报异常
                //1、从.NET6开始，当托管服务中发生未处理的异常的时候，程序就会自动停止并退出，
                //可以把HostOptions.BackgroundServiceExceptionBehavior设置为Ignore，程序会忽略异常，而不是停止程序
                //不过不推荐采用默认设置，因为“异常应该被妥善处置，而不是被忽略”
                //2、要在ExecuteAsync方法中把代码用try...catch包裹起来，当发生异常的时候，记录日志中或发警报等。
                //string s = null;
                //s.ToString();

                await Task.Delay(1000);//不能用sleep
                await Console.Out.WriteLineAsync(txt);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"服务器中有未处理异常：{ex.Message}");
            }
        }
    }
}
