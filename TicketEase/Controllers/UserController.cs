using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketEase.Application.DTO;
using TicketEase.Application.Interfaces.Repositories;
using TicketEase.Application.Interfaces.Services;
using TicketEase.Domain;

namespace TicketEase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudinaryServices _cloudinaryServices;
        private readonly IMapper _mapper;
        private readonly IUserServices _userServices;
        private readonly ILogger<UserController> _logger;

        public UserController(IUnitOfWork unitOfWork, ICloudinaryServices cloudinaryServices,
           IMapper mapper,IUserServices userServices, ILogger<UserController> logger)
        {
            _unitOfWork = unitOfWork;
            _cloudinaryServices = cloudinaryServices;
            _mapper = mapper;
             _userServices = userServices;
            _logger = logger;
        }

        [HttpPatch("photo/{id}")]
        public async Task<IActionResult> UpdateUserPhotoByUserId(string id, [FromForm] UpdatePhotoDTO model)
        {
            try
            {
                var user = _unitOfWork.UserRepository.GetUserById(id);
                if (user == null)
                    return NotFound("User not found");
                var file = model.PhotoFile;
                if (file == null || file.Length <= 0)
                    return BadRequest("Invalid file size");
                _mapper.Map(model, user);
                var imageUrl = await _cloudinaryServices.UploadContactImage(id, file);
                // Update the user's photo URL in the repository
                user.ImageUrl = imageUrl;
                _unitOfWork.UserRepository.Update(user);
                // Return the updated URL
                return Ok(new { Url = imageUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
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


