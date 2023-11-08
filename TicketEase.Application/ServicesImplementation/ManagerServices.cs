using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TicketEase.Application.DTO;
using TicketEase.Application.Interfaces.Repositories;
using TicketEase.Application.Interfaces.Services;
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
                return ApiResponse<string>.Success("Success", "Manager updated successfully", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while editing a Manager");
                return ApiResponse<string>.Failed(new List<string> { "Error: " + ex.Message });
            }
        }

        //public ApiResponse<List<Manager>> GetAllManagerByPagination(int page, int perPage, string user)
        //{
        //    try
        //    {
        //        perPage = perPage <= 0 ? 10 : perPage;
        //        page = page <= 0 ? 1 : page;


        //        IEnumerable<Manager> allManagers = _unitOfWork.ManagerRepository.GetAll();

        //        Func<Manager, string> nameSelector = manager => manager.CompanyName;
        //        Func<Manager, string> emailSelector = manager => manager.BusinessEmail;

        //        var paginationResult = Pagination<Manager>.GetPager(allManagers, perPage, page, nameSelector, emailSelector);

        //        var pagedManagers = paginationResult.Data.ToList();
        //        var totalPageCount = paginationResult.TotalPageCount;
        //        var currentPage = paginationResult.CurrentPage;

        //        var responseMessage = $"Page {currentPage} of {totalPageCount}";

        //        return ApiResponse<List<Manager>>.Success(pagedManagers, responseMessage, 200);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error occurred while retrieving paged managers");
        //        return ApiResponse<List<Manager>>.Failed(new List<string> { "Error: " + ex.Message });
        //    }
        //}

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
                return ApiResponse<EditManagerDto>.Success(Manager, "Manager retrieved successfully", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving a ticket");
                return ApiResponse<EditManagerDto>.Failed(new List<string> { "Error: " + ex.Message });
            }
        }

        public ApiResponse<string> AddManager(Manager managerDTO)
        {
            try
            {
                var managerEntity = _mapper.Map<Manager>(managerDTO);
                _unitOfWork.ManagerRepository.AddManager(managerEntity);
                _unitOfWork.SaveChanges();
                _logger.LogInformation("Manager added successfully");
                return ApiResponse<string>.Success("Success", "Manager added successfully", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a manager");
                return ApiResponse<string>.Failed(new List<string> { "Error: " + ex.Message });
            }
        }
    }
}
