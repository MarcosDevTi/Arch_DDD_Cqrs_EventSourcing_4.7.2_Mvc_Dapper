using Arch.CqrsClient.Commands.Customers;
using Arch.Infra.DataDapper.Sqlite;
using Arch.Infra.Shared.Cqrs.Commands;
using Dapper;
using System;
using System.Text;

namespace Arch.CqrsHandlers.Customers
{
    public class CustomerCommandHandler :
        ICommandHandler<CreateCustomer>
    {
        private readonly DapperContext _context;

        public CustomerCommandHandler(DapperContext context)
        {
            _context = context;
        }

        public void Handle(CreateCustomer command)
        {
            var idAddress = Guid.NewGuid();
            var idCustomer = Guid.NewGuid();
            AddAddress(command, idAddress);
            AddCustomer(command, idAddress, idCustomer);
        }

        private void AddCustomer(CreateCustomer command, Guid idAddress, Guid idCustomer)
        {
            var insertCustomer = new StringBuilder()
                .AppendLine("INSERT INTO CUSTOMERS")
                .AppendLine("(Id, AddressId, BirthDate, EmailAddress, FirstName, LastName, Score, CreatedDate)")
                .AppendLine("VALUES ")
                .AppendLine($"(\"{idCustomer}\", \"{idAddress}\", \"{command.BirthDate}\",  \"{command.Email}\", \"{command.FirstName}\",")
                .AppendLine($"\"{command.LastName}\", {command.Score}, \"{DateTime.Now}\")")
                .ToString();

            _context.Connection.Execute(insertCustomer);

        }
        private void AddAddress(CreateCustomer command, Guid idAddress)
        {
            var insertAddress = new StringBuilder()
                   .AppendLine("INSERT INTO ADDRESSES")
                   .AppendLine("(Id, City, Number, Street, ZipCode, CreatedDate)")
                   .AppendLine("VALUES ")
                   .AppendLine($"(\"{idAddress}\", \"{command.City}\", \"{command.Number}\", \"{command.Street}\", \"{command.ZipCode}\", \"{DateTime.Now}\")").ToString();

            _context.Connection.Execute(insertAddress);
        }
    }
}
