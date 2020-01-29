namespace Arch.Infra.DataDapper
{
    public static class Settings
    {
        public const string ConnectionString = @"oracle connection";
        public const string ConnectionStringsSqlite = @"Data Source=ArchDatabase.db";
        public const string ConnectionStringSqliteLocal = @"Data Source=|DataDirectory|\ArchDatabase.db";
        public const string ConnectionStringSqliteEventSourcing = @"Data Source=|DataDirectory|\ArchEventSourcingDatabase.db";
    }
}
