using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketEase.Domain;
using TicketEase.Domain.Entities;

namespace TicketEase.Application.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<ApiResponse<string>> ForgotPasswordAsync(string email);
        Task<ApiResponse<string>> ResetPasswordAsync(string email, string token, string newPassword);
        Task<ApiResponse<string>> ChangePasswordAsync(AppUser user, string currentPassword, string newPassword);
    }
}
