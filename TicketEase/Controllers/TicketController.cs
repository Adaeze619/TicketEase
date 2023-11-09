using Microsoft.AspNetCore.Mvc;
using TicketEase.Application.Interfaces.Services;
using TicketEase.Domain;
using TicketEase.Domain.Enums;

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
        public async Task<ActionResult<ApiResponse<bool>>>
        DeleteTicketById(string ticketId)
        {
            try
            {
                if (string.IsNullOrEmpty(ticketId))
                {
                    return BadRequest("Ticket ID is required.");
                }

                var response = await _ticketService.DeleteTicketByIdAsync(ticketId);

                if (response.Succeeded)
                {
                    return Ok(response);
                }

                return NotFound(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpGet("status-by-pagination/{status}")]
        public async Task<IActionResult> GetTicketsByStatusWithPagination(Status status, int page, int pageSize)
        {
            try
            {

                if (page <= 0 || pageSize <= 0)
                {
                    return BadRequest("Invalid page or pageSize values.");
                }

                var result = await _ticketService.GetTicketsByStatusWithPagination(status, page, pageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
    }
}
