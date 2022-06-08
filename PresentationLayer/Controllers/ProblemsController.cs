using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogicLayer.Extensions;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Resources;
using BusinessLogicLayer.Resources.ProblemResources;
using BusinessLogicLayer.Services.Communication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ProblemsController : Controller
    {
        private readonly IProblemService _problemService;

        public ProblemsController(IProblemService problemService)
        {
            _problemService = problemService;
        }

        [Authorize(Roles = "TeamLeader")]
        [HttpGet]
        public async Task<IEnumerable<ProblemResource>> GetAllAsync()
        {
            IEnumerable<ProblemResource> problems = await _problemService.ListAsync();

            return problems;
        }
        
        [Authorize(Roles = "CommonEmployee")]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveProblemResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            ProblemResponse result = await _problemService.SaveAsync(resource);
            if (!result.Success)
                return BadRequest(result.Message);
        
            return Ok(result.ProblemResource);
        }
        
        [Authorize(Roles = "CommonEmployee")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveProblemResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            ProblemResponse result = await _problemService.UpdateAsync(id, resource);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.ProblemResource);
        }
        
        [Authorize(Roles = "TeamLeader")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            ProblemResponse result = await _problemService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.ProblemResource);
        }

        [Authorize(Roles = "CommonEmployee")]
        [HttpGet("{id:int}")]
        public async Task<ProblemResource> FindByIdAsync(int id)
        {
            ProblemResource resource = await _problemService.FindByIdAsync(id);

            return resource;
        }

        [Authorize(Roles = "CommonEmployee")]
        [HttpGet("byCreationTime/{CreationTime:DateTime}")]
        public IEnumerable<ProblemResource> FindByCreationTime(DateTime creationTime)
        {
            IEnumerable<ProblemResource> resources = _problemService.FindByCreationTime(creationTime);
            return resources;
        }
        
        [Authorize(Roles = "CommonEmployee")]
        [HttpGet("byChangeTime/{ChangeTime:DateTime}")]
        public IEnumerable<ProblemResource> FindByChangeTime(DateTime changeTime)
        {
            IEnumerable<ProblemResource> resources = _problemService.FindByChangeTime(changeTime);
            return resources;
        }
        
        [Authorize(Roles = "CommonEmployee")]
        [HttpGet("byEmployee/{EmployeeId:int}")]
        public IEnumerable<ProblemResource> FindByEmployeeId(int employeeId)
        {
            IEnumerable<ProblemResource> resources = _problemService.FindByEmployeeId(employeeId);
            return resources;
        }

        [Authorize(Roles = "CommonEmployee")]
        [HttpPost("comments")]
        public async Task<IActionResult> PostAsync([FromBody] CommentResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            CommentResponse result = await _problemService.SaveCommentAsync(resource);
        
            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result.CommentResource);
        }
        
        [Authorize(Roles = "TeamLeader")]
        [HttpGet("comments")]
        public async Task<IEnumerable<CommentResource>> GetAllCommentsAsync()
        {
            IEnumerable<CommentResource> comments = await _problemService.CommentsListAsync();

            return comments;
        }
    }
}