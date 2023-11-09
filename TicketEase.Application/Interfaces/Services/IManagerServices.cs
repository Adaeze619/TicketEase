using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketEase.Application.Interfaces.Services
{
    public interface IManagerServices
    {
        Task DeactivateManager(string id);
        Task ActivateManager(string id);
    }
}
