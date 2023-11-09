using TicketEase.Application.Interfaces.Repositories;
using TicketEase.Application.Interfaces.Services;

namespace TicketEase.Application.ServicesImplementation
{
    public class ManagerServices : IManagerServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public ManagerServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task DeactivateManager(string id)
        {
            var manager = _unitOfWork.UserRepository.GetUserById(id);
            if (manager != null)
            {
                manager.IsActive = false;
                _unitOfWork.UserRepository.UpdateUser(manager); // Save changes to deactivate
            }
        }
        public async Task ActivateManager(string id)
        {
            var manager = _unitOfWork.UserRepository.GetUserById(id);
            if (manager != null)
            {
                manager.IsActive = true;
                _unitOfWork.UserRepository.UpdateUser(manager); // Save changes to deactivate
            }

        }
    }
}
