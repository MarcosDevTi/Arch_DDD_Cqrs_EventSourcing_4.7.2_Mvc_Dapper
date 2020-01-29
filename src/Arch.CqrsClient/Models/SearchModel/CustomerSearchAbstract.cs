using Arch.CqrsClient.Attributes;
using System;

namespace Arch.CqrsClient.Models.SearchModel
{
    public class CustomerSearchAbstract
    {
        public PropertySearch<string> FirstName { get; set; }
        public PropertySearch<string> LastName { get; set; }
        [ColumnName("EmailAddress")]
        public PropertySearch<string> Email { get; set; }
        public PropertySearch<DateTime?> BirthDate { get; set; }
        public PropertySearch<int?> Score { get; set; }
    }
}
