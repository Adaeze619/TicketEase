using AutoMapper;
using Microsoft.Extensions.Logging;
using Serilog;
using TicketEase.Application.DTO;
using TicketEase.Application.Interfaces.Services;
using TicketEase.Common.Utilities;
using TicketEase.Domain;
using TicketEase.Domain.Entities;

namespace TicketEase.Application.ServicesImplementation
{
    public class ManagerService : IManagerService
    {
        private readonly IEmailServices _emailServices;

        public ManagerService(IEmailServices emailServices)
        {
            _emailServices = emailServices;
        }

        public async Task<ApiResponse<bool>> SendManagerInformationToAdminAsync(ManagerInfoCreateDto managerDto)
        {
            try
            {
                var mailRequest = new MailRequest
                {
                    ToEmail = managerDto.AdminEmail,
                    Subject = "Manager Information",
                    Body = $"Business Email: {managerDto.BusinessEmail}\n" +
                           $"Company Name: {managerDto.CompanyName}\n" +
                           $"Reason to Onboard: {managerDto.ReasonToOnboard}"
                   
                };

                await _emailServices.SendEmailAsync(mailRequest);
                return ApiResponse<bool>.Success(true, "Manager information sent to admin successfully", 200);
            }
            catch (Exception ex)
            {
               
                Log.Error(ex, "An error occurred while sending manager information to admin");
                return ApiResponse<bool>.Failed(new List<string> { "Error: " + ex.Message });
            }
        }
    }
}
