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
           var response= _managerServices.DeactivateManager(id);
            if(response== null)
            return NoContent(); // 204 No Content
            return Ok(response);
        }

        [HttpPut("activate/{id}")]
        public IActionResult ActivateManager(string id)
        {
            var response = _managerServices.ActivateManager(id);
            if(response==null)
            return NoContent();
            return Ok(response);
        }
    }
}