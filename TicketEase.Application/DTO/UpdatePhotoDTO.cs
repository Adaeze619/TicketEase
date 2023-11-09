using Microsoft.AspNetCore.Http;

namespace TicketEase.Application.DTO
{
    public class UpdatePhotoDTO
    {
        public IFormFile PhotoFile { get; set; }
    }
}