using Arch.CqrsClient.Commands.Customers;
using Arch.Infra.DataDapper.Sqlite;
using Arch.Infra.Shared.Cqrs.Commands;
using Arch.Infra.Shared.DomainNotifications;
using Dapper;
using System;
using System.Text;

namespace Arch.CqrsHandlers.Customers
{
    public class CustomerCommandHandler : CommandHandlerBase,
        ICommandHandler<CreateCustomer>,
        ICommandHandler<UpdateCustomer>
    {
        private readonly DapperContext _context;
        private readonly EventSourcingDapperContext _eventSourcingContext;

        public CustomerCommandHandler(
            DapperContext context,
            IDomainNotification notifications,
            EventSourcingDapperContext eventSourcingContext)
            : base(context, eventSourcingContext, notifications)
        {
            _context = context;
        }

        public void Handle(CreateCustomer command)
        {
            ValidateCommand(command);
            var exists = _context.Exists(command.Email, "EmailAddress", "Customers");
            var actionName = "Create Customer";
            if (exists)
            {
                AddNotification(actionName, "The customer e-mail has already been taken.");
            }

            if (InvalidTransaction()) return;

            var idAddress = Guid.NewGuid();
            var idCustomer = Guid.NewGuid();

            AddAddress(command, idAddress);
            AddCustomer(command, idAddress, idCustomer);
            command.AggregateId = idCustomer;
            AddEventSourcing(command, actionName);
        }

        public void Handle(UpdateCustomer command)
        {
            ValidateCommand(command);
            command.AggregateId = command.Id;
        }

        private void AddCustomer(CreateCustomer command, Guid idAddress, Guid idCustomer)
        {
            var insertCustomer = new StringBuilder()
                .AppendLine("INSERT INTO CUSTOMERS")
                .AppendLine("(Id, AddressId, BirthDate, EmailAddress, FirstName, LastName, Score, CreatedDate)")
                .AppendLine("VALUES ")
                .AppendLine($"(\"{idCustomer.ToString()}\", \"{idAddress}\", \"{command.BirthDate}\",  \"{command.Email}\", \"{command.FirstName}\",")
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
                   .AppendLine($"(\"{idAddress.ToString()}\", \"{command.City}\", \"{command.Number}\", \"{command.Street}\", \"{command.ZipCode}\", \"{DateTime.Now}\")").ToString();

            _context.Connection.Execute(insertAddress);
        }

        
    }
}
