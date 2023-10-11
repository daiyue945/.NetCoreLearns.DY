using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace 领域事件发布的时机
{
    public abstract class BaseEntity : IDomainEvents
    {
        [NotMapped]//=Ignore()
        private  IList<INotification> events = new List<INotification>();
        public void AddDomianEvent(INotification notif)
        {
            events.Add(notif);
        }

        public void ClearDomainEvents()
        {
            events.Clear();
        }

        public IEnumerable<INotification> GetDomainEvents()
        {
            return events;
        }
    }
}
