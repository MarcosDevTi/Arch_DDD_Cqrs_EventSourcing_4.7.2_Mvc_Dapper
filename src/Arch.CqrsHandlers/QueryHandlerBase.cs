using Arch.Infra.DataDapper.Sqlite;

namespace Arch.CqrsHandlers
{
    public class QueryHandlerBase
    {
        private readonly ArchContext _context;

        public QueryHandlerBase(ArchContext context)
        {
            _context = context;
        }


    }
}
