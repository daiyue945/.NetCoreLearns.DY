using MediatR;

namespace 领域事件发布的时机
{
    public record NewUserNotification(string UserName, DateTime time) : INotification;

}
