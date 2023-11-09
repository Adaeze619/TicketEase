using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketEase.Application.DTO.Project;
using TicketEase.Common.Utilities;
using TicketEase.Domain;
using TicketEase.Domain.Entities;

namespace TicketEase.Application.Interfaces.Services
{
    public interface IProjectServices
    {
        Task<ApiResponse<ProjectReponseDto>> CreateProjectAsync(string boardId, ProjectRequestDto project);
        Task<ApiResponse<ProjectReponseDto>> UpdateProjectAsync(string boardId, string projectId, UpdateProjectRequestDto projectUpdate);
        ApiResponse<Project> GetProjectById(string projectId);
        Task<PageResult<IEnumerable<Project>>> GetProjectsByBoardId(string boardId, int perPage, int page);
    }
}
