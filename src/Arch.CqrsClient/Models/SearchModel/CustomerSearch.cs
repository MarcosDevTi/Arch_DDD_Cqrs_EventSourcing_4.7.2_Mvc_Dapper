using Arch.CqrsClient.Attributes;
using System;

namespace Arch.CqrsClient.Models.SearchModel
{
    public class CustomerSearch
    {
        public PropertyComparable<string> FirstName { get; set; }
        public string LastName { get; set; }
        [ColumnName("EmailAddress")]
        public PropertyComparable<string> Email { get; set; }
        public PropertyComparable<DateTime?> BirthDate { get; set; }
        public PropertyComparable<int?> Score { get; set; }
    }
}
