using System.ComponentModel.DataAnnotations;
using TicketEase.Domain.Enums;

namespace TicketEase.Domain.Entities
{
    public class TicketUser
    {
        [Key]
        public string ContactId { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        public string? ImageUrl { get; set; } 

        public bool isActive { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime Updated { get; set; }
        [Required]
        public string? UserId { get; set; }
       // public User User { get; set; }
      
      
    }
}
