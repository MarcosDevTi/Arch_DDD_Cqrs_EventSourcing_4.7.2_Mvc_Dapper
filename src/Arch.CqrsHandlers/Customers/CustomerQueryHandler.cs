using Arch.CqrsClient.Models;
using Arch.CqrsClient.Queries;
using Arch.Infra.DataDapper.Sqlite;
using Arch.Infra.Shared.Cqrs.Query;
using Dapper;
using System.Collections.Generic;
using System.Text;

namespace Arch.CqrsHandlers.Customers
{
    public class CustomerQueryHandler :
        IQueryHandler<GetCustomers, IEnumerable<CustomerItemIndex>>
    {
        private readonly DapperContext _context;

        public CustomerQueryHandler(DapperContext context)
        {
            _context = context;
        }

        public IEnumerable<CustomerItemIndex> Handle(GetCustomers query)
        {
            var sb = new StringBuilder();
            sb.AppendLine("SELECT (((\"_.Address\".\"Number\" || ' ') || \"_.Address\".\"Street\") || ' ') || \"_.Address\".\"City\" AS \"Address\", \"_\".\"BirthDate\", \"_\".\"EmailAddress\" AS \"Email\", \"_\".\"Id\", \"_\".\"FirstName\" AS \"Name\", \"_\".\"Score\"");
            sb.AppendLine("FROM \"Customers\" AS \"_\"");
            sb.AppendLine("INNER JOIN \"Addresses\" AS \"_.Address\" ON \"_\".\"AddressId\" = \"_.Address\".\"Id\"");
            sb.AppendLine("ORDER BY \"Name\"");
            sb.AppendLine($"LIMIT '{query.Take}' OFFSET '0'");
            var sql = sb.ToString();
            var result = _context.Connection.Query<CustomerItemIndex>(sql);


            return result;
        }
    }
}
