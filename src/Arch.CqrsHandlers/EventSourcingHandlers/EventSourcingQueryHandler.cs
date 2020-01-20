using Arch.CqrsClient.Queries.EventSourcing;
using Arch.Infra.DataDapper.Sqlite;
using Arch.Infra.Shared.Cqrs.Query;
using Arch.Infra.Shared.EventSourcing;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arch.CqrsHandlers.EventSourcingHandlers
{
    public class EventSourcingQueryHandler :
        IQueryHandler<GetCustomerHistory, IReadOnlyList<object>>
    {
        private readonly EventSourcingDapperContext _eventSourcingContext;
        public EventSourcingQueryHandler(EventSourcingDapperContext eventSourcingContext)
        {
            _eventSourcingContext = eventSourcingContext;
        }
        public IReadOnlyList<object> Handle(GetCustomerHistory query) =>
            GetEventSourcingEvent(query.AggregateId).ToList();

        protected IEnumerable<object> GetEventSourcingEvent(Guid aggregateId)
        {
            return GetEventEntities(aggregateId).Select(_ => _.ReadToObject(_, typeof(GetCustomerHistory).Assembly));
        }

        public IEnumerable<EventEntity> GetEventEntities(Guid aggregateId)
        {
            var sb = new StringBuilder();
            sb.AppendLine("SELECT \"x\".\"Id\", \"x\".\"Action\", \"x\".\"AggregateId\", \"x\".\"Assembly\", \"x\".\"Data\", \"x\".\"When\", \"x\".\"Who\"");
            sb.AppendLine("FROM \"EventEntities\" AS \"x\"");
            sb.AppendLine($"WHERE \"x\".\"AggregateId\" = '{aggregateId}'");
            sb.AppendLine("ORDER BY \"x\".\"When\"");
            var sql = sb.ToString();

            return _eventSourcingContext.Connection.Query<EventEntity>(sql);
        }

    }
}
