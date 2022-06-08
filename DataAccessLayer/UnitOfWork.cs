using System.Threading.Tasks;
using DataAccessLayer.EF;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ReportsDbContext _context;

        public UnitOfWork(ReportsDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync() => await _context.SaveChangesAsync();
    }
}