namespace Arch.Infra.DataDapper
{
    public static class Settings
    {
        public const string ConnectionString = @"oracle connection";
        public const string ConnectionStringsSqlite = @"Data Source=ArchDatabase.db";
        public const string ConnectionStringSqliteLocal = @"Data Source=C:\projects\ArchDDDCqrsDapper\src\Arch.Mvc\App_Data\ArchDatabase.db";
        public const string ConnectionStringSqliteEventSourcing = @"Data Source=C:\projects\Arch_DDD_Cqrs_EventSourcing_4.7.2_Mvc\src\Arch.Mvc\App_Data\ArchEventSourcingDatabase.db";
    }
}
