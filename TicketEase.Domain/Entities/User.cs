using Microsoft.AspNetCore.Identity;
using TicketEase.Domain.Enums;

namespace TicketEase.Domain.Entities
{
    public class User : IdentityUser
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }        
        public string Address { get; set; }
        public string State { get; set; }
        public Gender Gender { get; set; }
        public string CloudinaryPublicId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Ticket> Tickets { get; set; }


    }
}
