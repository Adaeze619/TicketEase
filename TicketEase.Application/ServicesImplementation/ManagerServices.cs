using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TicketEase.Application.DTO;
using TicketEase.Application.Interfaces.Repositories;
using TicketEase.Application.Interfaces.Services;
using TicketEase.Common.Utilities;
using TicketEase.Domain;
using TicketEase.Domain.Entities;

namespace TicketEase.Application.ServicesImplementation
{
    public class ManagerServices : IManagerServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ManagerServices> _logger;

        public ManagerServices(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ManagerServices> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public ApiResponse<string> EditManager(string userId, EditManagerDto editManagerDto)
        {
            try
            {
                var existingManager = _unitOfWork.ManagerRepository.GetManagerById(userId);

                if (existingManager == null)
                {
                    _logger.LogWarning("Manager with such Id does not exist");
                    return ApiResponse<string>.Failed(new List<string> { "Manager not found" });
                }

                _mapper.Map(editManagerDto, existingManager);

                _unitOfWork.ManagerRepository.GetManagerById(userId);
                _unitOfWork.SaveChanges();

                _logger.LogInformation("Manager updated successfully");
                return ApiResponse<string>.Success("Success", "Manager updated successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while editing a Manager");
                return ApiResponse<string>.Failed(new List<string> { "Error: " + ex.Message });
            }
        }

        public async Task<ApiResponse<PageResult<IEnumerable<Manager>>>> GetAllManagerByPagination(int page, int perPage)
        {
            try
            {
                perPage = perPage <= 0 ? 10 : perPage;
                page = page <= 0 ? 1 : page;

                var managers = _unitOfWork.ManagerRepository.GetAll();

                var pagedManagers = await Pagination<Manager>.GetPager(
                    managers,
                    perPage,
                    page,
                    manager => manager.CompanyName,
                    manager => manager.BusinessEmail);

                var response = new ApiResponse<PageResult<IEnumerable<Manager>>>(
                    true,
                    "Operation successful",
                    200,
                    new PageResult<IEnumerable<Manager>>
                    {
                        Data = pagedManagers.Data.ToList(),
                        TotalPageCount = pagedManagers.TotalPageCount,
                        CurrentPage = pagedManagers.CurrentPage,
                        PerPage = perPage,
                        TotalCount = pagedManagers.TotalCount
                    },
                    new List<string>());

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving paged managers");
                return ApiResponse<PageResult<IEnumerable<Manager>>>.Failed(new List<string> { "Error: " + ex.Message });
            }
        }

        public ApiResponse<EditManagerDto> GetManagerById(string userId)
        {
            try
            {
                var existingManager = _unitOfWork.ManagerRepository.GetManagerById(userId);

                if (existingManager == null)
                {
                    _logger.LogWarning("Manager with found ");
                    return ApiResponse<EditManagerDto>.Failed(new List<string> { "Manager not found" });
                }

                var Manager = _mapper.Map<EditManagerDto>(existingManager);
                _logger.LogInformation("Manager retrieved successfully");
                return ApiResponse<EditManagerDto>.Success(Manager, "Manager retrieved successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving a ticket");
                return ApiResponse<EditManagerDto>.Failed(new List<string> { "Error: " + ex.Message });
            }
        }

       
    }
}
