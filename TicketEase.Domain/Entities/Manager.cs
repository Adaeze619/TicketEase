using System.ComponentModel.DataAnnotations.Schema;

namespace TicketEase.Domain.Entities
{
    public class Manager
    {
        public Guid Id { get; set; }
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }      
        public User Users { get; set; }
        public string CompanyName { get; set; }
        public string BusinessEmail { get; set; }
        public string BusinessPhone { get; set; }
        public string CompanyAddress { get; set; }
        public string State { get; set; }
    }
}
