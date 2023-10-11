using MediatR;

namespace 领域事件发布的时机
{
    public class NewUserHandler : NotificationHandler<NewUserNotification>
    {
        protected override void Handle(NewUserNotification notification)
        {
            Console.WriteLine($"新建用户:{notification.UserName}");
        }
    }
}
