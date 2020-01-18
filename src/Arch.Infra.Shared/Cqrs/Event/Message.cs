using Arch.Infra.Shared.Cqrs.Commands;
using System;

namespace Arch.Infra.Shared.Cqrs.Event
{
    public class Message : ICommand
    {
        public Message()
        {
            Action = GetType().Name;

        }

        public string Action { get; protected set; }
        public Guid AggregateId { get; set; }
        public string Who { get; set; }
    }
}
