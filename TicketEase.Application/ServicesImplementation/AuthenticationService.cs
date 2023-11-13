using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Asn1.Ocsp;
using TicketEase.Application.Interfaces.Services;
using TicketEase.Domain;
using TicketEase.Domain.Entities;

namespace TicketEase.Application.ServicesImplementation
{
    public class AuthenticationService : IAuthenticationService
    {
      //  private readonly IHttpContextAccessor _httpContextAccessor;
       // private readonly IUrlHelper _urlHelper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly EmailServices _emailServices;
        private readonly ILogger _logger;

        public AuthenticationService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IOptions<EmailSettings> emailSettings, ILogger<AuthenticationService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailServices = new EmailServices(emailSettings);
            _logger = logger;
           // _httpContextAccessor = httpContextAccessor;
          //  _urlHelper = urlHelper;
        }


        public async Task<ApiResponse<string>> RegisterAsync(AppUser user)
        {
            try
            {
                // Customize the email address using the user's first name and last name
                var customizedEmail = $"{user.FirstName.ToLower()}.{user.LastName.ToLower()}@tickerease.com";

                // Generate a random password (you can replace this with your own logic)
                var password = GenerateRandomPassword(user.UserName);

                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    // Generate email confirmation token
                    var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    // Get the confirmation link
                   // var confirmationLink = GenerateConfirmationLink(user, emailConfirmationToken);

                    // Customize your email body here
                    var mailRequest = new MailRequest
                    {
                        ToEmail = user.Email, // Original email address that the user registered with
                        Subject = "Registration Successful",
                        Body = $"Dear {user.UserName},<br><br>" +
                                $"Thank you for registering with our website. Your new credentials are as follows:<br>" +
                                $"Email: {customizedEmail}<br>" + // Use the customized email in the email body
                                $"Password: {password}<br>" 
                             //   $"Please confirm your email by clicking the link: {confirmationLink}"
                    };

                    // Use your email service or library to send the email
                    await _emailServices.SendHtmlEmailAsync(mailRequest);

                    return new ApiResponse<string>(true, "Registration successful. Confirmation email sent.", 200, null, new List<string>());
                }
                else
                {
                    var errors = new List<string>();
                    foreach (var error in result.Errors)
                    {
                        errors.Add(error.Description);
                    }
                    return new ApiResponse<string>(false, "Registration failed", 404, null, errors);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during registration");
                var errorList = new List<string> { ex.Message };
                return ApiResponse<string>.Failed(false, "Error occurred during registration", 500, errorList);
            }
        }

        // Generate a random password based on the username
        private string GenerateRandomPassword(string UserName)
        {
            // Ensure the username is not null or empty
            if (string.IsNullOrEmpty(UserName) || UserName.Length < 5)
            {
                // Handle invalid username as needed
                throw new ArgumentException("Invalid username");
            }

            // Take the first 5 letters of the username
            string firstFiveLetters = UserName.Substring(0, 5);

            // Generate 6 random numbers
            Random random = new Random();
            string randomNumbers = new string(Enumerable.Repeat("0123456789", 6)
                                            .Select(s => s[random.Next(s.Length)])
                                            .ToArray());

            // Combine the first 5 letters of the username with 6 random numbers
            return $"{firstFiveLetters}{randomNumbers}";
        }




        // Construct confirmation link
        //private string GenerateConfirmationLink(AppUser user, string emailConfirmationToken)
        //{
        //    return _urlHelper.Action("ConfirmEmailAsync", "Account", new { email = user.Email, token = emailConfirmationToken }, protocol: _httpContextAccessor.HttpContext.Request.Scheme);
        //}




      


        public async Task<ApiResponse<string>> LoginAsync(string email, string password)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    return new ApiResponse<string>(false, "User not found.", 404, null, new List<string>());
                }

                var result = await _signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);

                switch (result)
                {
                    case { Succeeded: true }:
                        return new ApiResponse<string>(true, "Login successful.", 200, new List<string>());

                    case { IsLockedOut: true }:
                        return new ApiResponse<string>(false, $"Account is locked out. Please try again later or contact support." +
                            $" You can unlock your account after {_userManager.Options.Lockout.DefaultLockoutTimeSpan.TotalMinutes} minutes.", 
                            403, null, new List<string>());

                    case { RequiresTwoFactor: true }:
                        return new ApiResponse<string>(false, "Two-factor authentication is required.", 401, null, new List<string>());

                    case { IsNotAllowed: true }:
                        return new ApiResponse<string>(false, "Login failed. Email confirmation is required.", 401, null, new List<string>());

                    default:
                        return new ApiResponse<string>(false, "Login failed. Invalid email or password.",
                            401, null, new List<string> { "Invalid email or password." });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during login");
                var errorList = new List<string> { ex.Message };
                return ApiResponse<string>.Failed(false, "Error occurred during login", 500, errorList);
            }
        }



        public async Task<ApiResponse<string>> ConfirmEmailAsync(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return new ApiResponse<string>(false, "User not found.", 404, null, new List<string>());
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                return new ApiResponse<string>(true, "Email confirmation successful.", 200, new List<string>());
            }

            // Handle confirmation failure
            return new ApiResponse<string>(false, "Email confirmation failed.", 400, null, new List<string>());
        }



        public async Task<ApiResponse<string>> ForgotPasswordAsync(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    return new ApiResponse<string>(false, "User not found or email not confirmed.", 404, null, new List<string>());
                }
                string token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var resetPasswordUrl = "https://localhost:7068/reset-password?email=" + Uri.EscapeDataString(email) + "&token=" + Uri.EscapeDataString(token);

                var mailRequest = new MailRequest
                {
                    ToEmail = email,
                    Subject = "Password Reset Instructions",
                    Body = $"Please reset your password by clicking <a href='{resetPasswordUrl}'>here</a>."
                };
                await _emailServices.SendHtmlEmailAsync(mailRequest);

                return new ApiResponse<string>(true, "Password reset email sent successfully.", 200, null, new List<string>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while resolving password change");
                var errorList = new List<string>();
                errorList.Add(ex.Message);
                return new ApiResponse<string>(true, "Error occurred while resolving password change", 500, null, errorList);
            }
        }

        public async Task<ApiResponse<string>> ResetPasswordAsync(string email, string token, string newPassword)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    return new ApiResponse<string>(false, "User not found.", 404, null, new List<string>());
                }
                var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

                if (result.Succeeded)
                {
                    return new ApiResponse<string>(true, "Password reset successful.", 200, null, new List<string>());
                }
                else
                {
                    return new ApiResponse<string>(false, "Password reset failed.", 400, null, result.Errors.Select(error => error.Description).ToList());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while resetting password");
                var errorList = new List<string> { ex.Message };
                return new ApiResponse<string>(true, "Error occurred while resetting password", 500, null, errorList);
            }           
        }

        public async Task<ApiResponse<string>> ChangePasswordAsync(AppUser user, string currentPassword, string newPassword)
        {
            try
            {
                var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

                if (result.Succeeded)
                {
                    return new ApiResponse<string>(true, "Password changed successfully.", 200, null, new List<string>());
                }
                else
                {
                    return new ApiResponse<string>(false, "Password change failed.", 400, null, result.Errors.Select(error => error.Description).ToList());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while changing password");
                var errorList = new List<string> { ex.Message };
                return new ApiResponse<string>(true, "Error occurred while changing password", 500, null, errorList);
            }
        }
    }
}
