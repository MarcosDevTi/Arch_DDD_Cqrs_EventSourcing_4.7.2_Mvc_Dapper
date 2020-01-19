using Arch.Infra.Shared.EventSourcing;
using Dapper;
using System;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace Arch.Infra.DataDapper.Sqlite
{
    public class EventSourcingDapperContext : IDisposable
    {
        public SQLiteConnection Connection { get; set; }

        public EventSourcingDapperContext()
        {
            Connection = new SQLiteConnection(Settings.ConnectionStringSqliteEventSourcing);
            Connection.Open();
        }

        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();
        }


        public void SaveEvent(EventEntity @event)
        {
            var sb = new StringBuilder();
            sb.AppendLine("INSERT INTO \"EventEntities\" " +
                "(\"Id\", \"Action\", \"AggregateId\", \"Assembly\", \"Data\", \"When\", \"Who\")");
            sb.AppendLine($"VALUES " +
                $"('{Guid.NewGuid()}', '{@event.Action}', '{@event.AggregateId}', '{@event.Assembly}',");
            sb.AppendLine($"'{@event.Data}', '{@event.When}', '{@event.Who}');");
            var sql = sb.ToString();

            Connection.Execute(sql);
        }
    }
}
