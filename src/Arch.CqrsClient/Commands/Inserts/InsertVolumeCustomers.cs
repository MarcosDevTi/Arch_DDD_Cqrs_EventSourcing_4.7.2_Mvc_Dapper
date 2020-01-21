using Arch.Infra.Shared.Cqrs.Commands;

namespace Arch.CqrsClient.Commands.Inserts
{
    public class InsertVolumeCustomers : ICommand
    {
        public InsertVolumeCustomers(int insertsCount)
        {
            InsertsCount = insertsCount;
        }
        public int InsertsCount { get; set; }
    }
}
