using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Resources.ReportResources;
using BusinessLogicLayer.Services.Communication;
using DataAccessLayer.Entities;
using DataAccessLayer.EStates;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReportService(IReportRepository reportRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReportResource>> ListAsync()
        {
            IEnumerable<Report> reports = await _reportRepository.ListAsync();
            IEnumerable<ReportResource> resources =
                _mapper.Map<IEnumerable<Report>, IEnumerable<ReportResource>>(reports);
            
            return resources;
        }

        public async Task<ReportResponse> SaveAsync(SaveReportResource saveReportResource)
        {
            try
            {
                Report report = _mapper.Map<SaveReportResource, Report>(saveReportResource);
                report.EReportState = EReportState.Active;
                await _reportRepository.AddAsync(report);
                await _unitOfWork.CompleteAsync();
                ReportResource reportResource = _mapper.Map<Report, ReportResource>(report);
                return new ReportResponse(reportResource);
            }
            catch (Exception ex)
            {
                return new ReportResponse($"An error occurred when saving the report: {ex.Message}");
            }
        }

        public async Task<ReportResponse> UpdateAsync(int id, SaveReportResource saveReportResource)
        {
            Report existingReport = await _reportRepository.FindByIdAsync(id);

            if (existingReport == null)
                return new ReportResponse("Report not found.");

            Report report = _mapper.Map<SaveReportResource, Report>(saveReportResource);
            
            existingReport.Description = report.Description;
            existingReport.EReportState = report.EReportState;
            
            try
            {
                _reportRepository.Update(existingReport);
                await _unitOfWork.CompleteAsync();
                ReportResource resource = _mapper.Map<Report, ReportResource>(report);
                return new ReportResponse(resource);
            }
            catch (Exception ex)
            {
                return new ReportResponse($"An error occurred when updating the report: {ex.Message}");
            }
        }

        public async Task<ReportResponse> DeleteAsync(int id)
        {
            Report existingReport = await _reportRepository.FindByIdAsync(id);

            if (existingReport == null)
                return new ReportResponse("Report not found.");
            
            try
            {
                _reportRepository.Remove(existingReport);
                await _unitOfWork.CompleteAsync();
                ReportResource resource = _mapper.Map<Report, ReportResource>(existingReport);
                return new ReportResponse(resource);
            }
            catch (Exception ex)
            {
                return new ReportResponse($"An error occurred when deleting the report: {ex.Message}");
            }
        }
    }
}