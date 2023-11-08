using TicketEase.Application.DTO;
using TicketEase.Application.Interfaces.Repositories;
using TicketEase.Domain;
using TicketEase.Domain.Entities;

namespace TicketEase.Application.ServicesImplementation
{
    public class ProjectServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectServices(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        public ApiResponse<ProjectResponseDto> DeleteAllProjects()
        {
            ApiResponse<ProjectResponseDto> response;
            try
            {
                List<Project> projects = _unitOfWork.ProjectRepository.GetProjects();
                _unitOfWork.ProjectRepository.DeleteAllProject(projects);
                response = new ApiResponse<ProjectResponseDto>(true, 200, "is successful");
                return response;
            }
            catch (Exception ex) 
            {
                response = new ApiResponse<ProjectResponseDto>(false, 500, "failed" + ex.InnerException);
                return response;
            }
        }
    }
}
