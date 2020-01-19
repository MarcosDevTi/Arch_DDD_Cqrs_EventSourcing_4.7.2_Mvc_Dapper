using Arch.CqrsClient.Commands.Customers;
using Arch.Infra.Shared.Cqrs.Query;
using System;

namespace Arch.CqrsClient.Queries.Customers
{
    public class GetCustomerForUpdate : IQuery<UpdateCustomer>
    {
        public GetCustomerForUpdate(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
