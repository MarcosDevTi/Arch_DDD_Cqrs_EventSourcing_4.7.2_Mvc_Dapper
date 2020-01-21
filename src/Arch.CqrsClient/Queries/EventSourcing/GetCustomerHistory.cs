using Arch.Infra.Shared.Cqrs.Query;
using System;
using System.Collections.Generic;

namespace Arch.CqrsClient.Queries.EventSourcing
{
    public class GetHistory : IQuery<IReadOnlyList<object>>
    {
        public GetHistory(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }
        public Guid AggregateId { get; set; }
    }
}
