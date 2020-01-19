using Arch.CqrsClient.Commands.Customers;
using Arch.CqrsClient.Models;
using Arch.CqrsClient.Queries;
using Arch.CqrsClient.Queries.Customers;
using Arch.Infra.DataDapper.Sqlite;
using Arch.Infra.Shared.Cqrs.Query;
using Dapper;
using System.Collections.Generic;
using System.Text;

namespace Arch.CqrsHandlers.Customers
{
    public class CustomerQueryHandler :
        IQueryHandler<GetCustomers, IEnumerable<CustomerItemIndex>>,
        IQueryHandler<GetCustomerForUpdate, UpdateCustomer>
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
            sb.AppendLine("LIMIT '5' OFFSET '0'");
            var sql = sb.ToString();
            var result = _context.Connection.Query<CustomerItemIndex>(sql);

            return result;
        }

        public UpdateCustomer Handle(GetCustomerForUpdate query)
        {
            var sb = new StringBuilder();
            sb.AppendLine("SELECT \"_\".\"Id\", \"_\".\"AddressId\", \"_\".\"BirthDate\", \"_\".\"CreatedDate\", " +
                "\"_\".\"EmailAddress\", \"_\".\"FirstName\", \"_\".\"LastName\", \"_\".\"Score\", \"_.Address\".\"Id\", " +
                "\"_.Address\".\"City\", \"_.Address\".\"CreatedDate\", \"_.Address\".\"Number\", \"_.Address\".\"Street\", " +
                "\"_.Address\".\"ZipCode\"");
            sb.AppendLine("FROM \"Customers\" AS \"_\"");
            sb.AppendLine("INNER JOIN \"Addresses\" AS \"_.Address\" ON \"_\".\"AddressId\" = \"_.Address\".\"Id\"");
            sb.AppendLine($"WHERE \"_\".\"Id\" = '{query.Id}'");
            sb.AppendLine("LIMIT 1");
            var sql = sb.ToString();
            return _context.Connection.QuerySingle<UpdateCustomer>(sql);
        }
    }
}
