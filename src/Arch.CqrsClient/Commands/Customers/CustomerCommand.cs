using Arch.Infra.Shared.Cqrs.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace Arch.CqrsClient.Commands.Customers
{
    public abstract class CustomerCommand : CommandAction
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public int Score { get; set; }

        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
    }
}
