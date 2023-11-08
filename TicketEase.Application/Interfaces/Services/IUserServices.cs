using TicketEase.Application.DTO;
using TicketEase.Domain;

namespace TicketEase.Application.Interfaces.Services
{
    public interface IUserServices
    {
        Task<ApiResponse<AppUserDto>> GetUserByIdAsync(string userId);
        Task<ApiResponse<bool>> UpdateUserAsync(string userId, UpdateUserDto userUpdateDto);
    }
}
