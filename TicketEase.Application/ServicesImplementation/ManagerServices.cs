using TicketEase.Application.Interfaces.Repositories;
using TicketEase.Application.Interfaces.Services;
using TicketEase.Domain;
using TicketEase.Domain.Entities;

namespace TicketEase.Application.ServicesImplementation
{
    public class ManagerServices : IManagerServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public ManagerServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string DeactivateManager(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return "Manager Id must be provided";
            }

            var manager = _unitOfWork.UserRepository.GetUserById(id);

            if (manager != null)
            {
                manager.IsActive = false;
                _unitOfWork.UserRepository.UpdateUser(manager);
                _unitOfWork.SaveChanges();// Save changes to deactivate

            }
            return $"Manager with Id {id} has been deactivated successfully";

        }

        
        public string ActivateManager(string id)
        {
            var manager = _unitOfWork.UserRepository.GetUserById(id);
            if (manager != null)
            {
                manager.IsActive = true;
                _unitOfWork.UserRepository.UpdateUser(manager);
                _unitOfWork.SaveChanges();// Save changes to deactivate
                return $"Manager with Id {id} has been activated successfully";
            }
            else
            {
                return "Manager not found";
            }


        }
    }
   
}

