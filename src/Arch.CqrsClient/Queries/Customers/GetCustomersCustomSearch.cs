using Arch.CqrsClient.Models;
using Arch.CqrsClient.Models.SearchModel;
using Arch.Infra.Shared.Cqrs.Query;
using System.Collections.Generic;

namespace Arch.CqrsClient.Queries.Customers
{
    public class GetCustomersCustomSearch : IQuery<IEnumerable<CustomerItemIndex>>
    {
        public CustomerSearch Search { get; set; }
    }
}
