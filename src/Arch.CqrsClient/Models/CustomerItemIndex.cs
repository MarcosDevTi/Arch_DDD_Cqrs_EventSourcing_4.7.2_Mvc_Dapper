using System;

namespace Arch.CqrsClient.Models
{
    public class CustomerItemIndex
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public int Score { get; set; }
        public string Address { get; set; }
    }
}
