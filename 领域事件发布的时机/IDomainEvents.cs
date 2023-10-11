using MediatR;

namespace 领域事件发布的时机
{
    public interface IDomainEvents
    {
        IEnumerable<INotification> GetDomainEvents();//获取所有注册事件
        void AddDomianEvent(INotification notif);//消息注册
        void ClearDomainEvents();//清理
    }
}
