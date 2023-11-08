using TicketEase.Domain;
using TicketEase.Domain.Entities;
using TicketEase.Domain.Enums;

namespace TicketEase.Application.Interfaces.Services
{
    public interface ITicketService
    {
        Task<ApiResponse<bool>> DeleteTicketByIdAsync(string ticketId);
        //Task<ApiResponse<Ticket>> GetTicketByStatusAsync(Status status);
    }
}

