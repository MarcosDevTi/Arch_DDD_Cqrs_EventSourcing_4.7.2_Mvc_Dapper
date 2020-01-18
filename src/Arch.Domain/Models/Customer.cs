using Arch.Domain.Core;
using Arch.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Arch.Domain.Models
{
    public class Customer : Entity
    {
        public Customer() { }

        public Customer(string firstName, string lastName, string email, DateTime birthDate, Guid? id = null)
        {
            Id = id == null ? Guid.NewGuid() : id.Value;
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = email;
            BirthDate = birthDate;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public int Score { get; set; }
        public DateTime BirthDate { get; set; }
        public Address Address { get; set; }
        public Guid AddressId { get; set; }
        public ICollection<Order> Orders { get; set; }

        public void OrdersAdd(Order order) => Orders.Add(order);

        public void OrdersAdd(IEnumerable<Order> orders)
        {
            foreach (var order in orders) Orders.Add(order);
        }

        public void UpdateAddress(string street, string number, string zipCode)
        {
            Address.Street = street;
            Address.Number = number;
            Address.ZipCode = zipCode;
        }
    }
}
