using Arch.Infra.DataDapper.Sqlite;

namespace Arch.CqrsHandlers
{
    public class QueryHandlerBase
    {
        private readonly DapperContext _context;

        public QueryHandlerBase(DapperContext context)
        {
            _context = context;
        }


    }
}
