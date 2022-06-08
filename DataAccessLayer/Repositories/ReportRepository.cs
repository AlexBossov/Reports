using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.EF;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class ReportRepository : BaseRepository, IReportRepository
    {
        public ReportRepository(ReportsDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Report>> ListAsync()
        {
            return await Context.Reports.
                Include(r => r.Employee)
                .ToListAsync();
        }

        public async Task AddAsync(Report report)
        {
            await Context.Reports.AddAsync(report);
        }

        public async Task<Report> FindByIdAsync(int id)
        {
            return await Context.Reports.FindAsync(id);
        }

        public void Update(Report report)
        {
            Context.Reports.Update(report);
        }

        public void Remove(Report report)
        {
            Context.Reports.Remove(report);
        }
    }
}