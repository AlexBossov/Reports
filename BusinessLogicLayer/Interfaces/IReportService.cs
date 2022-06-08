using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogicLayer.Resources.ReportResources;
using BusinessLogicLayer.Services.Communication;

namespace BusinessLogicLayer.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<ReportResource>> ListAsync();
        Task<ReportResponse> SaveAsync(SaveReportResource saveReportResource);
        Task<ReportResponse> UpdateAsync(int id, SaveReportResource saveReportResource);
        Task<ReportResponse> DeleteAsync(int id);
    }
}