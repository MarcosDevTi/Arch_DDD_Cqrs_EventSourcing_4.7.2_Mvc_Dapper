using Arch.Infra.Shared.Cqrs.Event;
using FluentValidation.Results;

namespace Arch.Infra.Shared.Cqrs.Contracts
{
    public abstract class CommandAction : Message
    {
        public ValidationResult ValidationResult { get; set; }
        public abstract bool IsValid();
    }
}
