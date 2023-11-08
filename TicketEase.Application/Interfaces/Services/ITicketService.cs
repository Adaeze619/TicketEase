<<<<<<< HEAD
﻿using TicketEase.Domain;
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

=======
﻿using TicketEase.Common.Utilities;
using TicketEase.Domain;
using TicketEase.Domain.Entities;

namespace TicketEase.Application.Interfaces.Services
{
	public interface ITicketService
	{
		Task<ApiResponse<PageResult<IEnumerable<Ticket>>>> GetTicketByUserId(string userId, int page, int perPage);
		Task<ApiResponse<PageResult<IEnumerable<Ticket>>>> GetTicketByProjectId(string projectId, int page, int perPage);
	}
}
>>>>>>> 2eb9a4ba010210a0e8797fb925d810e9d9808ef9
