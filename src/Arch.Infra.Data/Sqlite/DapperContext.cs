using System;
using System.Data;
using System.Data.SQLite;

namespace Arch.Infra.DataDapper.Sqlite
{
    public class DapperContext : IDisposable
    {
        public SQLiteConnection Connection { get; set; }

        public DapperContext()
        {
            Connection = new SQLiteConnection(Settings.ConnectionStringSqliteLocal);
            Connection.Open();
        }

        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
    }
}
