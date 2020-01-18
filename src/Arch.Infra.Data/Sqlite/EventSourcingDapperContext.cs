using System;
using System.Data;
using System.Data.SQLite;

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
    }
}
