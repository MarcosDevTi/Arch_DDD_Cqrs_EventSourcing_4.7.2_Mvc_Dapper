using Arch.CqrsClient.Commands.Customers;
using Arch.CqrsClient.Extensions;
using Arch.CqrsClient.Models;
using Arch.CqrsClient.Queries.Customers;
using Arch.Infra.DataDapper.Sqlite;
using Arch.Infra.Shared.Cqrs.Query;
using Arch.Infra.Shared.Pagination;
using Dapper;
using System.Collections.Generic;
using System.Text;

namespace Arch.CqrsHandlers.Customers
{
    public class CustomerQueryHandler :
        IQueryHandler<GetCustomerForUpdate, UpdateCustomer>,
        IQueryHandler<GetCustomersPaging, PagedResult<CustomerItemIndex>>,
        IQueryHandler<GetCustomersCustomSearchAbstract, IEnumerable<CustomerItemIndex>>,
        IQueryHandler<GetCustomersCustomSearch, IEnumerable<CustomerItemIndex>>
    {
        private readonly ArchContext _context;

        public CustomerQueryHandler(ArchContext context)
        {
            _context = context;
        }

        private int CustomerCount()
        {
            var sql = "SELECT COUNT(*) FROM CUSTOMERS";
            return _context.Connection.QuerySingleOrDefault<int>(sql);
        }

        public PagedResult<CustomerItemIndex> Handle(GetCustomersPaging query)
        {
            var sb = new StringBuilder();
            sb.AppendLine("SELECT " +
                "(((\"Address\".\"Number\" || ' ') || \"Address\".\"Street\") || ' ') || \"Address\".\"City\" AS \"Address\", " +
                "\"_\".\"BirthDate\", \"_\".\"EmailAddress\" AS \"Email\", \"_\".\"Id\", \"_\".\"FirstName\" AS \"Name\", \"_\".\"Score\"");
            sb.AppendLine("FROM \"Customers\" AS \"_\"");
            sb.AppendLine("INNER JOIN \"Addresses\" AS \"Address\" ON \"_\".\"AddressId\" = \"Address\".\"Id\"");
            sb.AppendLine($"ORDER BY {query.Paging.SortColumn ?? "_.FirstName"} {query.Paging.SortDirection}");
            sb.AppendLine($"LIMIT '{query.Paging.PageSize}' OFFSET '{query.Paging.PageIndex}'");
            var sql = sb.ToString();
            var items = _context.Connection.Query<CustomerItemIndex>(sql);

            return new PagedResult<CustomerItemIndex>(items, CustomerCount(), query.Paging);
        }

        public UpdateCustomer Handle(GetCustomerForUpdate query)
        {
            var sb = new StringBuilder();
            sb.AppendLine("SELECT \"_\".\"Id\", \"_\".\"AddressId\", \"_\".\"BirthDate\", \"_\".\"EmailAddress\" AS \"Email\", " +
                "\"_\".\"FirstName\", \"_\".\"LastName\", \"_\".\"Score\", \"_.Address\".\"City\", " +
                "\"_.Address\".\"Number\", \"_.Address\".\"Street\", \"_.Address\".\"ZipCode\"");
            sb.AppendLine("FROM \"Customers\" AS \"_\"");
            sb.AppendLine("INNER JOIN \"Addresses\" AS \"_.Address\" ON \"_\".\"AddressId\" = \"_.Address\".\"Id\"");
            sb.AppendLine($"WHERE \"_\".\"Id\" = '{query.Id}'");
            sb.AppendLine("LIMIT 1");
            var sql = sb.ToString();
            return _context.Connection.QuerySingle<UpdateCustomer>(sql);
        }

        public IEnumerable<CustomerItemIndex> Handle(GetCustomersCustomSearchAbstract query)
        {
            var sb = new StringBuilder();
            sb.AppendLine("SELECT \"_\".\"Id\", \"_\".\"AddressId\", \"_\".\"BirthDate\", \"_\".\"EmailAddress\" AS \"Email\", " +
                "\"_\".\"FirstName\", \"_\".\"LastName\", \"_\".\"Score\", \"_.Address\".\"City\", " +
                "\"_.Address\".\"Number\", \"_.Address\".\"Street\", \"_.Address\".\"ZipCode\"");
            sb.AppendLine("FROM \"Customers\" AS \"_\"");
            sb.AppendLine("INNER JOIN \"Addresses\" AS \"_.Address\" ON \"_\".\"AddressId\" = \"_.Address\".\"Id\"");
            var sql = sb.ToString();
            var quere = sql + query.Search.GetWhereSql("_");

            return _context.Connection.Query<CustomerItemIndex>(sql);
        }

        public IEnumerable<CustomerItemIndex> Handle(GetCustomersCustomSearch query)
        {
            var sb = new StringBuilder();
            sb.AppendLine("SELECT \"_\".\"Id\", \"_\".\"AddressId\", \"_\".\"BirthDate\", \"_\".\"EmailAddress\" AS \"Email\", " +
                "\"_\".\"FirstName\", \"_\".\"LastName\", \"_\".\"Score\", \"_.Address\".\"City\", " +
                "\"_.Address\".\"Number\", \"_.Address\".\"Street\", \"_.Address\".\"ZipCode\"");
            sb.AppendLine("FROM \"Customers\" AS \"_\"");
            sb.AppendLine("INNER JOIN \"Addresses\" AS \"_.Address\" ON \"_\".\"AddressId\" = \"_.Address\".\"Id\"");
            var sql = sb.ToString();
            var quere = sql + query.Search.GetWhereSql("_");

            return _context.Connection.Query<CustomerItemIndex>(sql);
        }
    }
}
