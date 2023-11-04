using System.ComponentModel.DataAnnotations.Schema;

namespace TicketEase.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public Guid CommentId { get; set; }
        [ForeignKey("Ticket")]
        public string TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public string Comments { get; set; }

    }
}
