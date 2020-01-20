using Arch.Infra.Shared.Cqrs.Query;
using System;
using System.Collections.Generic;

namespace Arch.CqrsClient.Queries.EventSourcing
{
    public class GetCustomerHistory : IQuery<IReadOnlyList<object>>
    {
        public GetCustomerHistory(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }
        public Guid AggregateId { get; set; }
    }
}
