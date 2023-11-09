using TicketEase.Application.DTO;
using TicketEase.Domain;

namespace TicketEase.Application.Interfaces.Services
{
    public interface IBoardServices
    {
        Task<ApiResponse<BoardResponseDto>> AddBoardAsync(BoardRequestDto boardRequestDto);
        Task<ApiResponse<BoardResponseDto>> UpdateBoardAsync(string boardId, BoardRequestDto boardRequestDto);
        ApiResponse<BoardResponseDto> DeleteAllBoards();
    }
}
