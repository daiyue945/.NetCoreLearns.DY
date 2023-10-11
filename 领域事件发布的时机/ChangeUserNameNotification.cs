using MediatR;

namespace 领域事件发布的时机
{
    public record ChangeUserNameNotification(string OldUserName, string NewUserName) : INotification;
}
