using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketEase.Application.DTO.Project;
using TicketEase.Application.Interfaces.Services;

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
        public async Task<IActionResult> CreateProject(string boardId, [FromBody] ProjectCreationDto project)
        {
            var response = await _projectServices.CreateProjectAsync(boardId, project);

            if (response.Succeeded)
            {
                return CreatedAtRoute(
                    "GetProject",
                    new { boardId, projectId = response.Data.Id },
                    response);
            }

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateProject(string boardId, string projectId, [FromBody] ProjectUpdateDto projectUpdate)
        {
            var response = await _projectServices.UpdateProjectAsync(boardId, projectId, projectUpdate);

            if (response.Succeeded)
            {
                return Ok(response);
            }

            return StatusCode(response.StatusCode, response);
        }

    }
}
