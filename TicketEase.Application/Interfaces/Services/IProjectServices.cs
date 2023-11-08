using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketEase.Application.DTO.Project;
using TicketEase.Domain;

namespace TicketEase.Application.Interfaces.Services
{
    public interface IProjectServices
    {
        Task<ApiResponse<ProjectDto>> CreateProjectAsync(string boardId, ProjectCreationDto project);
        Task<ApiResponse<ProjectDto>> UpdateProjectAsync(string boardId, string projectId, ProjectUpdateDto projectUpdate);
    }
}
