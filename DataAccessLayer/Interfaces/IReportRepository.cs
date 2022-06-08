using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Interfaces
{
    public interface IReportRepository
    {
        Task<IEnumerable<Report>> ListAsync();
        Task AddAsync(Report report);
        public Task<Report> FindByIdAsync(int id);
        public void Update(Report report);
        public void Remove(Report report);
    }
}