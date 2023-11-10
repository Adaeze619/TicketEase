using AutoMapper;
using Microsoft.Extensions.Logging;
using TicketEase.Application.DTO;
using TicketEase.Application.Interfaces.Repositories;
using TicketEase.Application.Interfaces.Services;
using TicketEase.Common.Utilities;
using TicketEase.Domain;
using TicketEase.Domain.Entities;

namespace TicketEase.Application.ServicesImplementation
{
	public class TicketService : ITicketService
	{
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<TicketService> _logger;

        public TicketService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<TicketService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
         
        public ApiResponse<bool> AddTicket(TicketDto ticketDTO)
        {
            try
            {
                var ticketEntity = _mapper.Map<Ticket>(ticketDTO);
                ticketEntity.TicketReference = TicketHelper.GenerateTicketReference();
                _unitOfWork.TicketRepository.AddTicket(ticketEntity);
                _unitOfWork.SaveChanges();
                _logger.LogInformation("Ticket added successfully");
                return ApiResponse<bool>.Success(true, "Ticket added successfully", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a ticket");
                return ApiResponse<bool>.Failed(new List<string> { "Error: " + ex.Message });
            }
        }


        public ApiResponse<bool> EditTicket(string ticketId, UpdateTicketDto updatedTicketDTO)
        {
            try
            {
                var existingTicket = _unitOfWork.TicketRepository.GetTicketById(ticketId);

                if (existingTicket == null)
                {
                    _logger.LogWarning("Ticket not found while trying to edit");
                    return ApiResponse<bool>.Failed(new List<string> { "Ticket not found" });
                }

                _mapper.Map(updatedTicketDTO, existingTicket);

                _unitOfWork.TicketRepository.UpdateTicket(existingTicket);
                _unitOfWork.SaveChanges();

                _logger.LogInformation("Ticket updated successfully");
                return ApiResponse<bool>.Success(true, "Ticket updated successfully", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while editing a ticket");
                return ApiResponse<bool>.Failed(new List<string> { "Error: " + ex.Message });
            }
        }
		

		public async Task<ApiResponse<PageResult<IEnumerable<Ticket>>>> GetTicketByProjectId(string projectId, int page, int perPage)
		{
			try
			{

				var matchingTickets = await _unitOfWork.TicketRepository
					.GetTicketByProjectId(ticket => ticket.ProjectId == projectId);

				if (matchingTickets == null)
				{
					return new ApiResponse<PageResult<IEnumerable<Ticket>>>(
						false, "No tickets found for the given project", 404, null, null);
				}

				var pagedTickets = await Pagination<Ticket>.GetPager(
					matchingTickets,
					perPage,
					page,
					ticket => ticket.Title,
					ticket => ticket.Id.ToString());

				return new ApiResponse<PageResult<IEnumerable<Ticket>>>(
					true, "Operation successful", 200, pagedTickets, new List<string>());
			}
			catch (Exception ex)
			{

				return new ApiResponse<PageResult<IEnumerable<Ticket>>>(
					false, "Error occurred", 500, null, new List<string> { ex.Message });
			}
		}


		public async Task<ApiResponse<PageResult<IEnumerable<Ticket>>>> GetTicketByUserId(string userId, int page, int perPage)
		{
			try
			{
	
				var userTickets = await _unitOfWork.TicketRepository
					.GetTicketByUserId(ticket => ticket.AppUserId == userId);

				if (userTickets == null || !userTickets.Any())
				{
					return new ApiResponse<PageResult<IEnumerable<Ticket>>>(
						false, "No tickets found for the given user", 404, null, null);
				}

				var pagedUserTickets = await Pagination<Ticket>.GetPager(
					userTickets,
					perPage,
					page,
					ticket => ticket.Title,
					ticket => ticket.Id.ToString());

				return new ApiResponse<PageResult<IEnumerable<Ticket>>>(
					true, "Operation successful", 200, pagedUserTickets, new List<string>());
			}
			catch (Exception ex)
			{
		
				return new ApiResponse<PageResult<IEnumerable<Ticket>>>(
					false, "Error occurred", 500, null, new List<string> { ex.Message });
			}
		}


	}
}
