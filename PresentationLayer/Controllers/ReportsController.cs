using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogicLayer.Extensions;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Resources.ReportResources;
using BusinessLogicLayer.Services.Communication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ReportsController: Controller
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [Authorize(Roles = "TeamLeader")]
        [HttpGet]
        public async Task<IEnumerable<ReportResource>> GetAllAsync()
        {
            IEnumerable<ReportResource> reports = await _reportService.ListAsync();

            return reports;
        }
        
        [Authorize(Roles = "CommonEmployee")]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveReportResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            
            ReportResponse result = await _reportService.SaveAsync(resource);
        
            if (!result.Success)
                return BadRequest(result.Message);
        
            return Ok(result.ReportResource);
        }
        
        [Authorize(Roles = "CommonEmployee")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveReportResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            ReportResponse result = await _reportService.UpdateAsync(id, resource);
            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result.ReportResource);
        }
        
        [Authorize(Roles = "TeamLeader")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            ReportResponse result = await _reportService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.ReportResource);
        }
    }
}