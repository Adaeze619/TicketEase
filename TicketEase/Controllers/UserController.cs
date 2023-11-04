using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using TicketEase.Application.Interfaces.Repositories;
using TicketEase.Application.Interfaces.Services;
using TicketEase.Domain.Entities;
using TicketEase.Persistence.Context;
using TicketEase.Persistence.Repositories;

namespace TicketEase.Controllers
{
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [ProducesResponseType(401)]
    [ProducesResponseType(201)]
    [ProducesResponseType(404)]

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly DataContext _context;
        private readonly ILogger<UserController> _logger;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;

        public UserController(DataContext context, ILogger<UserController> logger, IEmailService emailService, IUserRepository userRepository)
        {
            _context = context;
            _logger = logger;
            _emailService = emailService;
            _userRepository = userRepository;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            if (_context.Users.Any(u => u.Email == request.Email))
            {
                return BadRequest("User already exists.");
            }

            CreatePasswordHash(request.Password,
                 out byte[] passwordHash,
                 out byte[] passwordSalt);

            var user = new User
            {
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                VerificationToken = CreateRandomToken()
            };



            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Generate a verification link
            // var verificationLink = $"https://yourwebsite.com/verify?token={user.VerificationToken}";

            // Send a registration email
            var mailRequest = new MailRequest
            {
                ToEmail = user.Email, // User's email address
                Subject = "Welcome to Your Website - Email Verification",
                Body = "Thank you for registering on our website. Please verify your email by clicking this link: $\"https://yourwebsite.com/verify?token={user.VerificationToken}\"",
            };

            await _emailService.SendEmailAsync(mailRequest);


            // Log successful registration and email sending
            _logger.LogInformation($"User registration successful for email: {user.Email}");
            return Ok("User successfully created! Please check your email for verification instructions.");


        }




        [HttpPost("login")]

        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Password is incorrect.");
            }

            if (user.VerifiedAt == null)
            {
                return BadRequest("Not verified!");
            }

            var mailRequest = new MailRequest
            {
                ToEmail = user.Email, // User's email address
                Subject = "Login Successful!!",
                Body = "You have successfully Login...If this was not you, click on this link to reset your password! https://yourwebsite.com/verify?token  ",
            };

            await _emailService.SendEmailAsync(mailRequest);

            _logger.LogInformation($"User Login successful: {user.Email}");
            return Ok($"Login Successful, {user.Email}! ");
        }

        [HttpPost("verify")]

        public async Task<IActionResult> Verify(string token)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.VerificationToken == token);
            if (user == null)
            {
                return BadRequest("Invalid token.");
            }

            user.VerifiedAt = DateTime.Now;
            await _context.SaveChangesAsync();


            var mailRequest = new MailRequest
            {
                ToEmail = user.Email, // User's email address
                Subject = "Account Verification!",
                Body = "You have successfully verified your email...If this was not you, click on this link to reset your password! https://yourwebsite.com/verify?token  ",
            };

            await _emailService.SendEmailAsync(mailRequest);

            _logger.LogInformation($"User successfully Verified: {user.Email}");
            return Ok($"Verification Successful, {user.Email}! ");


        }

        [HttpPost("forgot-password")]

        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            user.PasswordResetToken = CreateRandomToken();
            user.ResetTokenExpires = DateTime.Now.AddDays(3);
            await _context.SaveChangesAsync();

            var mailRequest = new MailRequest
            {
                ToEmail = user.Email, // User's email address
                Subject = "Forgot Password?",
                Body = " Click on the link below to reset your password  https://yourwebsite.com/verify?token  ",
            };

            await _emailService.SendEmailAsync(mailRequest);

            _logger.LogInformation($"Forgot password successfully initiated: {user.Email}");
            return Ok($"You may now reset your password, {user.Email}! ");


        }

        [HttpPost("reset-password")]

        public async Task<IActionResult> ResettPassword(ResetPasswordRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == request.Token);
            if (user == null || user.ResetTokenExpires < DateTime.Now)
            {
                return BadRequest("Invalid Token.");
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.PasswordResetToken = request.Token;
            user.ResetTokenExpires = null;

            await _context.SaveChangesAsync();



            var mailRequest = new MailRequest
            {
                ToEmail = user.Email, // User's email address
                Subject = "Password Reset!",
                Body = " You have successfully reset your password.. Kindly Click here https://yourwebsite.com/verify?token  to Login",
            };

            await _emailService.SendEmailAsync(mailRequest);

            _logger.LogInformation($"Forgot password successfully initiated: {user.Email}");
            return Ok($"Password successfully reset, {user.Email}! ");

        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }



        [HttpPatch("image/{contactId}")]
        public async Task<ActionResult> UpdateImage(string Id, IFormFile image)
        {
            if (image == null)
            {
                return BadRequest("Image file is required");
            }
            if (image.Length <= 0)
            {
                return BadRequest("Image file is empty");
            }

            var response = await _userRepository.UploadContactImage(Id, image);

            if (response == "success")
            {
                return Ok("User image updated successfully");
            }
            else if (response == "User not found")
            {
                return NotFound("User not found");
            }
            else
            {
                return StatusCode(500, response);
            }
        }




    }
}
