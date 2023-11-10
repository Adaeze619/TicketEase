using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketEase.Domain;

namespace TicketEase.Application.Interfaces.Services
{
    public interface IManagerService
    {
        Task<ApiResponse<bool>> SendManagerInformationToAdminAsync(ManagerInfoCreateDto managerDto);
    }
}
