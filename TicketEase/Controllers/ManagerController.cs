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
            try
            {
                var response = _managerServices.DeactivateManager(id);
                if (response == null)
                return NotFound();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
          
        }

        [HttpPut("activate/{id}")]
        public IActionResult ActivateManager(string id)
        {
            try
            {
                var response = _managerServices.ActivateManager(id);
                if (response == null)
                    return NotFound();
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}