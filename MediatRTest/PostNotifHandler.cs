using MediatR;

namespace MediatRTest
{
    /// <summary>
    ///监听者进行消息处理，要实现NotificationHandler<TNotification>接口
    /// </summary>
    public class PostNotifHandler : NotificationHandler<PostNotification>
    {
        protected override void Handle(PostNotification notification)
        {
            //收到消息后出发以下代码
            Console.WriteLine($"1111:{notification.Body}已收到");
        }
    }
}
