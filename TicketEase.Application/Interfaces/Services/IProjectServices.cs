using TicketEase.Application.DTO.Project;
using TicketEase.Domain;

namespace TicketEase.Application.Interfaces.Services
{
    public interface IProjectServices
    {
        Task<ApiResponse<ProjectReponseDto>> CreateProjectAsync(string boardId, ProjectRequestDto project);
        Task<ApiResponse<ProjectReponseDto>> UpdateProjectAsync(string boardId, string projectId, UpdateProjectRequestDto projectUpdate);
        ApiResponse<ProjectReponseDto> DeleteAllProjects();
    }
}
