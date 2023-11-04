using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketEase.Domain.Enums;

namespace TicketEase.Domain.Entities
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public string? VerificationToken { get; set; } //has nothing to do with jwt token
        public DateTime? VerifiedAt { get; set; }
        public string PasswordResetToken { get; set; } = string.Empty; //for forgot password, a check if the user exist
        public DateTime? ResetTokenExpires { get; set; } //expiration time for the token above
        public string? ImageUrl { get; set; }



         

    }
}
