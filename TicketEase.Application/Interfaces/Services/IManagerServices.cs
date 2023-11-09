using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketEase.Application.Interfaces.Services
{
    public interface IManagerServices
    {
        string DeactivateManager(string id);
        string ActivateManager(string id);
    }
}
