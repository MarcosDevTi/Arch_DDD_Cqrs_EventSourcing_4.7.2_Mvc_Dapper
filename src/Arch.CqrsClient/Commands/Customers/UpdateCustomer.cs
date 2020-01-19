using Arch.CqrsClient.Commands.Customers.Validations;
using System;

namespace Arch.CqrsClient.Commands.Customers
{
    public class UpdateCustomer : CustomerCommand
    {
        public Guid Id { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new UpdateCustomerValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
