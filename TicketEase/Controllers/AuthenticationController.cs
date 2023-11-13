using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TicketEase.Application.DTO;
using TicketEase.Application.Interfaces.Services;
using TicketEase.Application.ServicesImplementation;
using TicketEase.Domain;
using TicketEase.Domain.Entities;

namespace TicketEase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUrlHelper _urlHelper;
        private readonly IAuthenticationService _authenticationService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailServices _emailServices;
        private readonly IMapper _mapper;


        public AuthenticationController(IUrlHelper urlHelper, IAuthenticationService authenticationService, UserManager<AppUser> userManager, IEmailServices emailServices, IMapper mapper)
        {
            _urlHelper = urlHelper;
            _authenticationService = authenticationService;
            _userManager = userManager;
            _emailServices = emailServices;
            _mapper = mapper;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _mapper.Map<AppUser>(model);

            var response = await _authenticationService.RegisterAsync(user);

            if (response.Succeeded)
            {

                return Ok(new ApiResponse<string>(true, response.Message, response.StatusCode, null, new List<string>()));
            }

            return BadRequest(new ApiResponse<string>(false, response.Message, response.StatusCode, null, response.Errors));
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }


            else
            {
                var user = _mapper.Map<AppUser>(login);


                var response = await _authenticationService.LoginAsync (user.Email, user.PasswordHash);

                if (response.Succeeded)
                {
                    var mailRequest = new MailRequest
                    {
                        ToEmail = user.Email, // User's email address
                        Subject = "Login Successful",
                        Body = $"Dear {user.UserName}, You have successfully Login! " +
                        $" If this was not you, kindly change your password and user authorization by clicking this link: $\"https://yourwebsite.com/verify?token={user.VerificationToken}\"",
                    };

                    await _emailServices.SendHtmlEmailAsync(mailRequest);

                    return Ok(new ApiResponse<string>(true, response.Message, response.StatusCode, null, new List<string>()));
                }

                return BadRequest(new ApiResponse<string>(false, response.Message, response.StatusCode, null, response.Errors));



            }
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string email, [FromQuery] string token)
        {
            var response = await _authenticationService.ConfirmEmailAsync(email, token);

            // Handle the response and return appropriate status
            if (response.Succeeded)
            {
                return Ok(new ApiResponse<string>(true, response.Message, 200, null, new List<string>()));
            }
            else
            {
                return BadRequest(new ApiResponse<string>(false, response.Message, response.StatusCode, null, response.Errors));
            }
        }





        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid model state.", 400, null, ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList()));
            }

            var response = await _authenticationService.ForgotPasswordAsync(model.Email);

            if (response.Succeeded)
            {
                return Ok(new ApiResponse<string>(true, response.Message, response.StatusCode, null, new List<string>()));
            }
            else
            {
                return BadRequest(new ApiResponse<string>(false, response.Message, response.StatusCode, null, response.Errors));
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid model state.", 400, null, ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList()));
            }

            var response = await _authenticationService.ResetPasswordAsync(model.Email, model.Token, model.NewPassword);

            if (response.Succeeded)
            {
                return Ok(new ApiResponse<string>(true, response.Message, response.StatusCode, null, new List<string>()));
            }
            else
            {
                return BadRequest(new ApiResponse<string>(false, response.Message, response.StatusCode, null, response.Errors));
            }
        }

        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid model state.", 400, null, ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList()));
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized(new ApiResponse<string>(false, "User not found.", 401, null, new List<string>()));
            }

            var response = await _authenticationService.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (response.Succeeded)
            {
                return Ok(new ApiResponse<string>(true, response.Message, response.StatusCode, null, new List<string>()));
            }
            else
            {
                return BadRequest(new ApiResponse<string>(false, response.Message, response.StatusCode, null, response.Errors));
            }
        }
    }
}
