using Dapper;
using System;
using System.Data;
using System.Data.SQLite;
using System.Text;

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

        public bool Exists(string value, string property, string table)
        {
            var sb = new StringBuilder();
            sb.AppendLine("SELECT CASE");
            sb.AppendLine("    WHEN EXISTS (");
            sb.AppendLine("        SELECT 1");
            sb.AppendLine($"        FROM \"{table}\" AS \"x\"");
            sb.AppendLine($"        WHERE \"x\".\"{property}\" = '{value}')");
            sb.AppendLine("    THEN 1 ELSE 0");
            sb.AppendLine("END");
            var sql = sb.ToString();

            return this.Connection.QuerySingle<bool>(sql);
        }
    }
}
