using MediatR;

namespace 领域事件发布的时机
{
    public class ChangeUserNameHandler : NotificationHandler<ChangeUserNameNotification>
    {
        protected override void Handle(ChangeUserNameNotification notification)
        {
            Console.WriteLine($"修改用户名成功:{notification.NewUserName}");
        }
    }
}
