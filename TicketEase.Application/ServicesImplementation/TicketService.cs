using TicketEase.Application.Interfaces.Repositories;
using TicketEase.Application.Interfaces.Services;
<<<<<<< HEAD
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
=======
using TicketEase.Common.Utilities;
using TicketEase.Domain;
using TicketEase.Domain.Entities;

namespace TicketEase.Application.ServicesImplementation
{
	public class TicketService : ITicketService
	{
		private readonly IUnitOfWork _unitOfWork;

		public TicketService(IUnitOfWork unitOfWork)
        {
			_unitOfWork = unitOfWork;
		}
        public async Task<ApiResponse<PageResult<IEnumerable<Ticket>>>> GetTicketByProjectId(string projectId, int page, int perPage)
		{
			var tickets = await _unitOfWork.TicketRepository.GetTicketByProjectId(ticket => ticket.ProjectId == projectId);

			// Use the Pagination class to paginate the data
			var pagedTickets = await Pagination<Ticket>.GetPager(
			tickets,
			perPage,
			page,
			ticket => ticket.Title,
			ticket => ticket.Id.ToString());


			//return pagedTickets;
			return new ApiResponse<PageResult<IEnumerable<Ticket>>>(true, "Operation succesful", 200, null, new List<string>());
		}

		public async Task<ApiResponse<PageResult<IEnumerable<Ticket>>>> GetTicketByUserId(string userId, int page, int perPage)
		{
			var tickets = await _unitOfWork.TicketRepository.GetTicketByUserId(ticket => ticket.AppUserId == userId);

			// Use the Paginatioo paginate the dat
			var pagedTickets = await Pagination<Ticket>.GetPager(
				tickets,
				perPage,
				page,
				ticket => ticket.Title,
				ticket => ticket.Id.ToString());

			return new ApiResponse<PageResult<IEnumerable<Ticket>>> (true, "Operation succesful", 200, null, new List<string>());
			//{
			//	Status = "Success",
			//	Data = pagedTickets
			//};

			//return pagedTickets;
		}
	}
>>>>>>> 2eb9a4ba010210a0e8797fb925d810e9d9808ef9
}
