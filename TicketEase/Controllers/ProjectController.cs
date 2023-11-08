using Microsoft.AspNetCore.Mvc;
using TicketEase.Application.ServicesImplementation;

namespace TicketEase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectServices _projectServices;

        public ProjectController(ProjectServices projectServices)
        {
            _projectServices = projectServices;
        }

        [HttpDelete("DeleteProjects")]
        public IActionResult DeleteAllProjects()
        {
            return Ok(_projectServices.DeleteAllProjects());
        }
    }
}
