using Microsoft.AspNetCore.Mvc;
using TicketEase.Application.Interfaces.Services;
<<<<<<< HEAD
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

=======

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

		[HttpGet("user/{userId}")]
		public async Task<IActionResult> GetTicketsByUserId(string userId, int page, int perPage)
		{

			var result = await _ticketService.GetTicketByUserId(userId, page, perPage);
			return Ok(result);

		}

		[HttpGet("project/{projectId}")]
		public async Task<IActionResult> GetTicketsByProjectId(string projectId, int page, int perPage)
		{
			var result = await _ticketService.GetTicketByProjectId(projectId, page, perPage);
			return Ok(result);

		}
	}
}
>>>>>>> 2eb9a4ba010210a0e8797fb925d810e9d9808ef9
