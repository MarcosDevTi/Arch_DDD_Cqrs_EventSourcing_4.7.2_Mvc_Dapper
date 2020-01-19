using System;

namespace Arch.CqrsClient.Commands.Customers
{
    public class DeleteCustomer : CustomerCommand
    {
        public Guid Id { get; set; }

        public override bool IsValid() => true;
    }
}
