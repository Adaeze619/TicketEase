using Microsoft.AspNetCore.Mvc;
using TicketEase.Application.Interfaces.Services;
using TicketEase.Domain;

namespace TicketEase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpDelete("{ticketId}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteTicketById(string ticketId)
        {
            var response = await _ticketService.DeleteTicketByIdAsync(ticketId);

            if (response.Succeeded)
            {
                return Ok(response);
            }

            return NotFound(response);
        }

        //[HttpGet("status/{status}")]
        //public async Task<ActionResult<ApiResponse<Ticket>>> GetTicketByStatus(Status status)
        //{
        //    var response = await _ticketService.GetTicketByStatusAsync(status);

        //    if (response.Succeeded)
        //    {
        //        return Ok(response);
        //    }

        //    return NotFound(response);
        //}
    }
}

