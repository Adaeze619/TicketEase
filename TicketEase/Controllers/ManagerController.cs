using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketEase.Application.Interfaces.Services;

namespace TicketEase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerServices _managerServices;
        public ManagerController(IManagerServices managerServices)
        {
            _managerServices = managerServices;
        }
        [HttpPut("deactivate/{id}")]
        public IActionResult DeactivateManager(string id)
        {
            _managerServices.DeactivateManager(id);
            return NoContent(); // 204 No Content
        }

        [HttpPut("activate/{id}")]
        public IActionResult ActivateManager(string id)
        {
            _managerServices.ActivateManager(id);
            return NoContent();
        }
    }
}