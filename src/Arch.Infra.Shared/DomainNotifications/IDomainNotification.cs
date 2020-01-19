using System.Collections.Generic;

namespace Arch.Infra.Shared.DomainNotifications
{
    public interface IDomainNotification
    {
        void Add(DomainNotification domainNotification);
        List<DomainNotification> GetNotifications();
        bool HasNotifications();
        void Dispose();
    }
}
