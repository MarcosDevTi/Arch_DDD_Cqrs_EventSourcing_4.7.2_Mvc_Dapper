using Arch.CqrsClient.Commands.Customers.Validations;
using System.ComponentModel.DataAnnotations;

namespace Arch.CqrsClient.Commands.Customers
{
    public class CreateCustomer : CustomerCommand
    {
        public override bool IsValid()
        {
            ValidationResult = new CreateCustomerValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
