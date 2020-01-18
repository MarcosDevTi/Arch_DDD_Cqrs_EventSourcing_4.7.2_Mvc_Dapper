using Arch.Infra.Shared.Cqrs.Commands;
using Arch.Infra.Shared.Cqrs.Query;

namespace Arch.Infra.Shared.Cqrs
{
    public interface IProcessor
    {
        void Send<TCommand>(TCommand command) where TCommand : ICommand;
        TResult Get<TResult>(IQuery<TResult> query);
    }
}
