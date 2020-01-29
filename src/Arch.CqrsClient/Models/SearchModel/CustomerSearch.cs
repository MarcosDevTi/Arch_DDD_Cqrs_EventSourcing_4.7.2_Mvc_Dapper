using Arch.CqrsClient.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arch.CqrsClient.Models.SearchModel
{
    public class CustomerSearch
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [ColumnName("EmailAddress")]
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? Score { get; set; }
    }
}
