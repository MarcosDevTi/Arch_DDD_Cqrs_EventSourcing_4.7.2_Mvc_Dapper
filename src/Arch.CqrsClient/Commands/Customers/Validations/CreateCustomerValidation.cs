namespace Arch.CqrsClient.Commands.Customers.Validations
{
    public class CreateCustomerValidation : CustomerCommandValidation<CreateCustomer>
    {
        public CreateCustomerValidation()
        {
            ValidateFirstName();
            ValidateLastName();
            ValidateBirthDate();
            ValidateEmail();
        }
    }
}
