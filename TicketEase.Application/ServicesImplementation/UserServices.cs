using AutoMapper;
using Serilog;
using TicketEase.Application.DTO;
using TicketEase.Application.Interfaces.Repositories;
using TicketEase.Application.Interfaces.Services;
using TicketEase.Domain;

namespace TicketEase.Application.ServicesImplementation
{
    public class UserServices : IUserServices
    {


        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<ApiResponse<AppUserDto>> GetUserByIdAsync(string userId)
        {
            try
            {
                var user = _unitOfWork.UserRepository.GetUserById(userId);

                if (user == null)
                {
                    return ApiResponse<AppUserDto>.Failed(false, "User not found.", 404, new List<string> { "User not found." });
                }

                var userDto = _mapper.Map<AppUserDto>(user);

                return ApiResponse<AppUserDto>.Success(userDto, "User found.", 200);
            }
            catch (Exception ex)
            {

                Log.Error(ex, "An error occurred while retrieving the user. UserID: {UserId}", userId);


                return ApiResponse<AppUserDto>.Failed(false, "An error occurred while retrieving the user.", 500, new List<string> { ex.Message });
            }
        }
        public async Task<ApiResponse<bool>> UpdateUserAsync(string userId, UpdateUserDto updateUserDto)
        {
            try
            {
                var user = _unitOfWork.UserRepository.GetUserById(userId);

                if (user == null)
                {
                    return ApiResponse<bool>.Failed(false, "User not found.", 404, new List<string> { "User not found." });
                }

                _mapper.Map(updateUserDto, user);

                _unitOfWork.UserRepository.UpdateUser(user);
                _unitOfWork.SaveChanges();

                return ApiResponse<bool>.Success(true, "User updated successfully.", 200);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while updating the user. UserID: {UserId}", userId);

                return ApiResponse<bool>.Failed(false, "An error occurred while updating the user.", 500, new List<string> { ex.Message });
            }
        }
    }
}