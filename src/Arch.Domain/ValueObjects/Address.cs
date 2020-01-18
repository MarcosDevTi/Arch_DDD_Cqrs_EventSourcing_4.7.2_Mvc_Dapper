using Arch.Domain.Core;
using Arch.Domain.Models;
using System;

namespace Arch.Domain.ValueObjects
{
    public class Address : Entity
    {
        public Address() { }
        public Address(string street, string number, string city, string zipCode, Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
            Street = street;
            Number = number;
            City = city;
            ZipCode = zipCode;
        }

        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public Customer Customer { get; set; }
    }
}
