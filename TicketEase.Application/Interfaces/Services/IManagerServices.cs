using TicketEase.Application.DTO;
using TicketEase.Common.Utilities;
using TicketEase.Domain;
using TicketEase.Domain.Entities;

namespace TicketEase.Application.Interfaces.Services
{
    public interface IManagerServices
    {
        ApiResponse<string> EditManager(string user, EditManagerDto managerDto);
        ApiResponse<EditManagerDto> GetManagerById(string user);
        Task<ApiResponse<PageResult<IEnumerable<Manager>>>> GetAllManagerByPagination(int page, int perPage);
        

    }
}
