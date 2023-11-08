using TicketEase.Application.DTO;
using TicketEase.Domain;
using TicketEase.Domain.Entities;

namespace TicketEase.Application.Interfaces.Services
{
    public interface IManagerServices
    {
        ApiResponse<string> EditManager(string user, EditManagerDto managerDto);
        ApiResponse<EditManagerDto> GetManagerById(string user);
        //ApiResponse<List<Manager>> GetAllManagerByPagination(int page, int perPage, string user);
        public ApiResponse<string> AddManager(Manager managerDTO);

    }
}
