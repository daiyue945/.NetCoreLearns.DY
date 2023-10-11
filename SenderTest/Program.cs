using RabbitMQ.Client;
using System.Text;

namespace SenderTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var connFactory = new ConnectionFactory();
            connFactory.HostName = "127.0.0.1";//RabbitMQ服务器地址
            connFactory.DispatchConsumersAsync = true;

            var connection = connFactory.CreateConnection();
            string exchangeName = "exchange1";
            string eventName = "Key1";

            while (true)
            {
                using (var channel = connection.CreateModel())
                {
                    var property=channel.CreateBasicProperties();
                    property.DeliveryMode = 2;//2表示消息持久化
                    channel.ExchangeDeclare(exchangeName, "direct");//声明交换机
                    byte[] bytes = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
                    channel.BasicPublish(exchangeName, routingKey: eventName,mandatory:true,basicProperties:property,body:bytes);//mandatory:true 没有匹配路由是退还
                    Console.WriteLine($"消息发送完成{DateTime.Now.ToString()}");
                }
                Thread.Sleep(1000);
            }
        }
    }
}