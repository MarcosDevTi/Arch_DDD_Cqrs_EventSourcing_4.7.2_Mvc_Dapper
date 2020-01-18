using Arch.CqrsClient.Models;
using Arch.Infra.Shared.Cqrs.Query;
using System.Collections.Generic;

namespace Arch.CqrsClient.Queries
{
    public class GetCustomers : IQuery<IEnumerable<CustomerItemIndex>>
    {
        public GetCustomers(int take)
        {
            Take = take;
        }
        public int Take { get; set; }
    }
}
