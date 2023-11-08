using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketEase.Application.DTO;
using TicketEase.Application.Interfaces.Services;
using TicketEase.Domain;

namespace TicketEase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserServices userServices, IMapper mapper, ILogger<UserController> logger)
        {
            _userServices = userServices;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDto updateUserDto)
        {
            var updateResult = await _userServices.UpdateUserAsync(id, updateUserDto);

            if (updateResult.Succeeded)
            {
                return Ok(new ApiResponse<bool>(true, "User updated successfully.", 200, true, null));
            }

            _logger.LogError("User update failed: {Message}", updateResult.Message);
            return BadRequest(new ApiResponse<bool>(false, "Failed to update user.", 400, false, updateResult.Errors));
        }



    }
}
