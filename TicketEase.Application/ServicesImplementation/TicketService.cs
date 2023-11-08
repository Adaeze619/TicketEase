using TicketEase.Application.Interfaces.Repositories;
using TicketEase.Application.Interfaces.Services;
using TicketEase.Domain;
using TicketEase.Domain.Entities;
using TicketEase.Domain.Enums;

namespace TicketEase.Application.ServicesImplementation
{
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TicketService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<bool>> DeleteTicketByIdAsync(string ticketId)
        {
            try
            {
                var existingTicket = _unitOfWork.TicketRepository.GetTicketById(ticketId);

                if (existingTicket == null)
                {
                    return ApiResponse<bool>.Failed(new List<string> { "Ticket not found." });
                }

                _unitOfWork.TicketRepository.DeleteTicket(existingTicket);
                _unitOfWork.SaveChanges();

                return ApiResponse<bool>.Success(true, "Ticket deleted successfully.", 200);
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                return ApiResponse<bool>.Failed(new List<string> { ex.Message });
            }
        }

        //public async Task<ApiResponse<Ticket>> GetTicketByStatusAsync(Status status)
        //{
        //    try
        //    {
        //        var tickets = _unitOfWork.TicketRepository.GetTicketsByStatus(status);

        //        if (tickets == null)
        //        {
        //            return ApiResponse<Ticket>.Failed(new List<string> { "No tickets found with the specified status." });
        //        }

        //        return ApiResponse<Ticket>.Success(tickets, "Tickets retrieved successfully.", 200);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle any exceptions
        //        return ApiResponse<Ticket>.Failed(new List<string> { ex.Message });
        //    }
        //}
    }
}
