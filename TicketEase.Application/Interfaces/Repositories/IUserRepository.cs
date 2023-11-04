using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketEase.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<string> UploadContactImage(string id, IFormFile image);
    }
}
