using Arch.Domain.Core;
using Dapper;
using System.Collections.Generic;

namespace Arch.Infra.DataDapper.Sqlite
{
    public class SqLiteBaseRepository<T>
       where T : Entity
    {
        private readonly DapperContext svbBdgSqliteContext;
        public SqLiteBaseRepository(DapperContext svbBdgSqliteContext)
        {
            this.svbBdgSqliteContext = svbBdgSqliteContext;
        }

        //public virtual long Creer(string sql, T entite)
        //{
        //    entite.Id = svbBdgSqliteContext.Connection.Query<long>(sql, entite).First();
        //    return entite.Id;
        //}

        public virtual IEnumerable<T> Obtenir(string sql, object parametres)
        {
            var result = this.svbBdgSqliteContext.Connection.Query<T>(sql, parametres);
            return result;
        }

        public virtual void Supprimer(string sql, int id)
        {
            this.svbBdgSqliteContext.Connection.Execute(sql, new { Id = id });
        }
    }
}
