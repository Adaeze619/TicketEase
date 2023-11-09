using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketEase.Application.DTO.Project;
using TicketEase.Application.Interfaces.Services;
using TicketEase.Common.Utilities;
using TicketEase.Domain.Entities;

namespace TicketEase.Controllers
{
    [Route("Project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectServices _projectServices;
        public ProjectController(IProjectServices projectServices)
        {
            _projectServices = projectServices;
        }

        [HttpPost("AddProject")]
        public async Task<IActionResult> CreateProject(string boardId, [FromBody] ProjectRequestDto project)
        {
            return Ok(_projectServices.CreateProjectAsync(boardId, project)); 
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateProject(string boardId, string projectId, [FromBody] UpdateProjectRequestDto projectUpdate)
        {
            return Ok(await _projectServices.UpdateProjectAsync(boardId, projectId, projectUpdate));
        }

        [HttpGet("{projectId}")]
        public IActionResult GetProjectById(string projectId)
        {
            return Ok(_projectServices.GetProjectById(projectId));

        }

        [HttpGet("GetProjectsByBoardId")]
        public Task<PageResult<IEnumerable<Project>>> GetProjectsByBoardId(string boardId, int perPage, int page)
        {
            var projects = _projectServices.GetProjectsByBoardId(boardId, perPage, page);

            return projects;
        }

    }
}
