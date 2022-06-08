using DataAccessLayer.EF;

namespace DataAccessLayer.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly ReportsDbContext Context;

        public BaseRepository(ReportsDbContext context)
        {
            Context = context;
        }
    }
}