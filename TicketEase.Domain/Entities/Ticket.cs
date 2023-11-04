using System.ComponentModel.DataAnnotations.Schema;
using TicketEase.Domain.Enums;

namespace TicketEase.Domain.Entities
{
    public class Ticket : BaseEntity
    {
        public string TicketReference { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public DateTime ResolvedAt { get; set; }
        public string AssignedTo { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }        
        public User Users { get; set; }
        [ForeignKey("ProjectId")]
        public Guid ProjectId { get; set; }     
        public Project Project { get; set; }
        [ForeignKey("CommentId")]
        public Guid CommentId { get; set; }        
        public Comment Comment { get; set; }

    }
}
