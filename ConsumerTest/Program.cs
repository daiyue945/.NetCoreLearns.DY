
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;

namespace ConsumerTest
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
            string queueName = "queue1";
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchangeName, "direct");//声明交换机
            channel.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);//声明队列
            channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: eventName);//和交换机绑定，把routingkey为eventName的消息转给队列


            //从队列中接收消息
            AsyncEventingBasicConsumer consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += Consumer_Received;
            channel.BasicConsume(queueName, autoAck: false, consumer: consumer);

            Console.WriteLine("按回车退出");
            Console.ReadLine();

            async Task Consumer_Received(object sender, BasicDeliverEventArgs _event)
            {
                try
                {
                    byte[] bytes = _event.Body.ToArray();
                    string text = Encoding.UTF8.GetString(bytes);
                    await Console.Out.WriteLineAsync($"{DateTime.Now}收到消息：{text}");
                    //DeliveryTag消息编号
                    channel.BasicAck(_event.DeliveryTag, false);//消息受否已接收处理
                    await Task.Delay(1000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    channel.BasicReject(_event.DeliveryTag, true);//异常返回，是否进行消息重发
                }

            }
        }
    }
}