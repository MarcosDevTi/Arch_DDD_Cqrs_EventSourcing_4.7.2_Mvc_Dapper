using Arch.Infra.DataDapper.Sqlite;
using Arch.Infra.Shared.Cqrs.Contracts;
using Arch.Infra.Shared.Cqrs.Event;
using Arch.Infra.Shared.DomainNotifications;
using Arch.Infra.Shared.EventSourcing;

namespace Arch.CqrsHandlers
{
    public class CommandHandlerBase
    {
        private readonly DapperContext _context;
        private readonly EventSourcingDapperContext _eventSourcingContext;
        private readonly IDomainNotification _notifications;

        public CommandHandlerBase(
            DapperContext context,
            EventSourcingDapperContext eventSourcingContext,
            IDomainNotification notifications)
        {
            _context = context;
            _notifications = notifications;
            _eventSourcingContext = eventSourcingContext;
        }

        protected void ValidateCommand(CommandAction cmd)
        {
            if (cmd.IsValid()) return;
            foreach (var error in cmd.ValidationResult.Errors)
                AddNotification(new DomainNotification(cmd.Action, error.ErrorMessage));
        }

        protected void AddNotification(DomainNotification notification) =>
           _notifications.Add(notification);

        protected void AddNotification(string action, string message) =>
           _notifications.Add(new DomainNotification(action, message));

        public void AddEventSourcing(Message command, string actionName, Message lastCommand = null)
        {
            var @event = EventEntity.GetEvent(actionName, command, "Marcos", lastCommand);
            _eventSourcingContext.SaveEvent(@event);
        }

        public bool InvalidTransaction()
        {
            return _notifications.HasNotifications();
        }
    }
}
