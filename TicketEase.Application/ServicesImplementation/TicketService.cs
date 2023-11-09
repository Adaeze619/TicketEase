using Microsoft.Extensions.Logging;
using TicketEase.Application.Interfaces.Repositories;
using TicketEase.Application.Interfaces.Services;
using TicketEase.Common.Utilities;
using TicketEase.Domain;
using TicketEase.Domain.Entities;
using TicketEase.Domain.Enums;

namespace TicketEase.Application.ServicesImplementation
{
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TicketService> _logger;

        public TicketService(IUnitOfWork unitOfWork, ILogger<TicketService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiResponse<bool>> DeleteTicketByIdAsync(string ticketId)
        {
            try
            {
                if (string.IsNullOrEmpty(ticketId))
                {
                    return ApiResponse<bool>.Failed(new List<string> { "Ticket ID is required." });
                }

                var existingTicket = _unitOfWork.TicketRepository.GetTicketById(ticketId);

                if (existingTicket == null)
                {
                    return ApiResponse<bool>.Failed(new List<string> { "Ticket not found." });
                }

                _unitOfWork.TicketRepository.DeleteTicket(existingTicket);
                _unitOfWork.SaveChanges();

                _logger.LogInformation($"Ticket with ID {ticketId} has been deleted successfully.");

                return ApiResponse<bool>.Success(true, "Ticket deleted successfully.", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the ticket.");

                return ApiResponse<bool>.Failed(new List<string> { "An error occurred while deleting the ticket." });
            }
        }

        public async Task<PageResult<IEnumerable<Ticket>>> GetTicketsByStatusWithPagination(Status status, int page, int pageSize)
        {
            try
            {
                var tickets = await _unitOfWork.TicketRepository.GetTicketsByStatusWithPagination(status, page, pageSize);

                _logger.LogInformation($"Retrieved {tickets.Data.Count()} tickets with status {status} (Page {page}, Page Size {pageSize}).");

                return tickets;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving tickets with pagination.");

                throw;
            }
        }
    }
}
