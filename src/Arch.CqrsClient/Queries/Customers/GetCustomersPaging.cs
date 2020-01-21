using Arch.CqrsClient.Models;
using Arch.Infra.Shared.Cqrs.Query;
using Arch.Infra.Shared.Pagination;

namespace Arch.CqrsClient.Queries.Customers
{
    public class GetCustomersPaging : IQuery<PagedResult<CustomerItemIndex>>
    {
        public GetCustomersPaging(Paging paging) => Paging = paging;

        public Paging Paging { get; set; }
    }
}
