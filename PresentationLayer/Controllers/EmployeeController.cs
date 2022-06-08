using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Extensions;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Resources.EmployeeResources;
using BusinessLogicLayer.Services.Communication;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [Authorize(Roles = "TeamLeader")]
        [HttpGet]
        public async Task<IEnumerable<EmployeeResource>> GetEmployees([FromQuery] EmployeeParameters employeeParameters)
        {
            return await _employeeService.ListAsync(employeeParameters);
        }
        
        [Authorize(Roles = "CommonEmployee")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            IEnumerable<EmployeeTreeResource> employeeTreeResources = await _employeeService.FindByIdAsync();
            return Ok(employeeTreeResources.ToList().Single(e => e.Id == id));
        }

        [Authorize(Roles = "CommonEmployee")]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveEmployeeResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            
            EmployeeResponse employeeResponse = await _employeeService.SaveAsync(resource);
        
            if (!employeeResponse.Success)
                return BadRequest(employeeResponse.Message);
        
            return Ok(employeeResponse.EmployeeResource);
        }
        
        [Authorize(Roles = "CommonEmployee")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveEmployeeResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            EmployeeResponse employeeResponse = await _employeeService.UpdateAsync(id, resource);

            if (!employeeResponse.Success)
                return BadRequest(employeeResponse.Message);

            return Ok(employeeResponse.EmployeeResource);
        }
        
        [Authorize(Roles = "CommonEmployee")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            EmployeeResponse employeeResponse = await _employeeService.DeleteAsync(id);

            if (!employeeResponse.Success)
                return BadRequest(employeeResponse.Message);

            return Ok(employeeResponse.EmployeeResource);
        }
    }
}