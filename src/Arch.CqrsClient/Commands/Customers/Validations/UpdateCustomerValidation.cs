namespace Arch.CqrsClient.Commands.Customers.Validations
{
    public class UpdateCustomerValidation : CustomerCommandValidation<UpdateCustomer>
    {
        public UpdateCustomerValidation()
        {
            ValidateFirstName();
            ValidateLastName();
            ValidateBirthDate();
        }
    }
}
