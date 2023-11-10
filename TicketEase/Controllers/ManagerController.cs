using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using TicketEase.Application;
using TicketEase.Application.Interfaces.Services;

namespace TicketEase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService managerService;

        public ManagerController(IManagerService managerService)
        {
            this.managerService = managerService;
        }

        [HttpPost("sendManagerInformation")]
        public async Task<IActionResult> SendManagerInformation([FromBody] ManagerInfoCreateDto managerDto)
        {
                return Ok(await managerService.SendManagerInformationToAdminAsync(managerDto));           
        }
    }
}
