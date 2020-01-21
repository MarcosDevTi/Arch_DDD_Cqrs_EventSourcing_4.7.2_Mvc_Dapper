using Arch.CqrsClient.Commands.Customers;
using Arch.CqrsClient.Commands.Inserts;
using Arch.Infra.DataDapper.Sqlite;
using Arch.Infra.Shared.Cqrs.Commands;
using Arch.Infra.Shared.DomainNotifications;
using Bogus;
using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arch.CqrsHandlers.Customers
{
    public class CustomerCommandHandler : CommandHandlerBase,
        ICommandHandler<CreateCustomer>,
        ICommandHandler<UpdateCustomer>,
        ICommandHandler<DeleteCustomer>,
        ICommandHandler<InsertVolumeCustomers>,
        ICommandHandler<TrucateCustomers>
    {
        private readonly ArchContext _context;
        private readonly EventSourcingContext _eventSourcingContext;

        public CustomerCommandHandler(
            ArchContext context,
            IDomainNotification notifications,
            EventSourcingContext eventSourcingContext)
            : base(context, eventSourcingContext, notifications)
        {
            _context = context;
            _eventSourcingContext = eventSourcingContext;
        }

        public void Handle(CreateCustomer command)
        {
            ValidateCommand(command);
            var exists = _context.Exists($"EmailAddress = '{command.Email}'", "Customers");
            var actionName = "Created Customer";
            if (exists)
            {
                AddNotification(actionName, "The customer e-mail has already been taken.");
            }

            if (InvalidTransaction()) return;

            var idCustomer = Guid.NewGuid();

            SaveCustomer(command, idCustomer);
            command.AggregateId = idCustomer;
            AddEventSourcing(command, actionName);
        }

        public void Handle(UpdateCustomer command)
        {
            ValidateCommand(command);
            var exists = _context.Exists(
                $"(\"EmailAddress\" = '{command.Email}') AND (\"Id\" <> '{command.Id}')", "Customers");

            var lastCommand = GetLastCommand(command.Id);

            UpdateCustomer(command);

            command.AggregateId = command.Id;
            AddEventSourcing(command, "Updated Customer", lastCommand);
        }

        public void Handle(DeleteCustomer command)
        {
            ValidateCommand(command);
            var lastCommand = GetLastCommand(command.Id);

            DeleteCustomer(command.Id);
            command.AggregateId = command.Id;
            AddEventSourcing(command, "Deleted Customer", new { });
        }

        private UpdateCustomer GetLastCommand(Guid commandId)
        {
            var sb = new StringBuilder()
            .AppendLine("SELECT " +
                "Id, FirstName, LastName, BirthDate, EmailAddress AS Email, Score, City, Number, Street, ZipCode " +
                "FROM (")
                .AppendLine("SELECT _.Id, _.AddressId, _.BirthDate, _.EmailAddress, " +
                    "_.FirstName, _.LastName, _.Score, Addresses.Id, Addresses.City, " +
                    "Addresses.CreatedDate, Addresses.Number, Addresses.Street, Addresses.ZipCode")
                .AppendLine("FROM Customers AS _")
                .AppendLine("INNER JOIN Addresses ON _.AddressId = Addresses.Id")
                .AppendLine($"WHERE _.Id = '{commandId}'")
                .AppendLine("LIMIT 1")

            .AppendLine(")");
            var sql = sb.ToString();

            return _context.Connection.QuerySingleOrDefault<UpdateCustomer>(sql);

        }

        private void SaveCustomer(CreateCustomer command, Guid idCustomer)
        {
            var idAddress = Guid.NewGuid();

            var insertCustomer = new StringBuilder()
                .AppendLine("INSERT INTO CUSTOMERS")
                .AppendLine("(Id, AddressId, BirthDate, EmailAddress, FirstName, LastName, Score, CreatedDate)")
                .AppendLine("VALUES ")
                .AppendLine($"(\"{idCustomer.ToString()}\", \"{idAddress}\", \"{command.BirthDate}\",  \"{command.Email}\"," +
                $" \"{command.FirstName}\",")
                .AppendLine($"\"{command.LastName}\", {command.Score}, \"{DateTime.Now}\")")
                .ToString();
            AddAddress(command, idAddress);
            _context.Connection.Execute(insertCustomer);

        }
        private void AddAddress(CreateCustomer command, Guid idAddress)
        {
            var insertAddress = new StringBuilder()
                   .AppendLine("INSERT INTO ADDRESSES")
                   .AppendLine("(Id, City, Number, Street, ZipCode, CreatedDate)")
                   .AppendLine("VALUES ")
                   .AppendLine($"(\"{idAddress.ToString()}\", \"{command.City}\", \"{command.Number}\", \"{command.Street}\"," +
                   $" \"{command.ZipCode}\", \"{DateTime.Now}\")").ToString();

            _context.Connection.Execute(insertAddress);
        }

        private void UpdateAddress(UpdateCustomer command)
        {
            var idAddress = GetIdAddress(command.Id);
            var sb = new StringBuilder()
            .AppendLine($"UPDATE \"Addresses\" SET " +
                $"\"Number\" = '{command.Number}', " +
                 $"\"Street\" = '{command.Street}', " +
                 $"\"City\" = '{command.City}', " +
                 $"\"ZipCode\" = '{command.ZipCode}' ")
            .AppendLine($"WHERE \"Id\" = '{idAddress}';")
            .AppendLine("SELECT changes();");
            var sql = sb.ToString();
            _context.Connection.Execute(sql);
        }

        public void UpdateCustomer(UpdateCustomer command)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"UPDATE \"Customers\" SET ");
            sb.AppendLine($" \"FirstName\" = '{command.FirstName}', ");
            sb.AppendLine($" \"EmailAddress\" = '{command.Email}', ");
            sb.AppendLine($" \"BirthDate\" = '{command.BirthDate}', ");
            sb.AppendLine($" \"Score\" = '{command.Score}' ");
            sb.AppendLine($"WHERE \"Id\" = '{command.Id}';");
            var sql = sb.ToString();
            UpdateAddress(command);
            _context.Connection.Execute(sql);
        }

        private Guid GetIdAddress(Guid customerId)
        {
            var sql = new StringBuilder()
            .AppendLine("SELECT AddressId FROM Customers")
            .AppendLine($"WHERE Id = '{customerId}'").ToString();
            return _context.Connection.QuerySingleOrDefault<Guid>(sql);
        }



        public void DeleteCustomer(Guid customerId)
        {
            var addressId = GetIdAddress(customerId);

            var sql = new StringBuilder()
            .AppendLine("DELETE FROM \"Customers\"")
            .AppendLine($"WHERE \"Id\" = '{customerId}';").ToString();

            DeleteAddress(addressId);
            _context.Connection.Execute(sql);
        }

        public void DeleteAddress(Guid addressId)
        {
            var sql = new StringBuilder()
            .AppendLine("DELETE FROM \"Addresses\"")
            .AppendLine($"WHERE \"Id\" = '{addressId}';").ToString();
            _context.Connection.Execute(sql);
        }

        public void Handle(InsertVolumeCustomers command)
        {
            var faker = new Faker();
            var idCustomer = Guid.NewGuid();

            var list = new List<CreateCustomer>();
            for (var i = 0; i < command.InsertsCount; i++)
            {
                var minDate = DateTime.Now.AddYears(-30);
                var maxDate = DateTime.Now.AddYears(-60);

                var customer = new CreateCustomer
                {
                    FirstName = faker.Name.FirstName(),
                    LastName = faker.Name.LastName(),
                    Email = faker.Person.Email,
                    BirthDate = faker.Date.Between(minDate, maxDate),

                    Street = faker.Address.StreetName(),
                    Number = faker.Address.BuildingNumber(),
                    City = faker.Address.City(),
                    ZipCode = faker.Address.ZipCode()
                };

                list.Add(customer);
            }
            list.ForEach(_ => SaveCustomer(_, Guid.NewGuid()));
            return;
        }

        public void Handle(TrucateCustomers command)
        {
            _context.Connection.Execute("DELETE FROM CUSTOMERS");
            _context.Connection.Execute("DELETE FROM ADDRESSES");
            _eventSourcingContext.Connection.Execute("DELETE FROM EVENTENTITIES");
        }
    }
}
