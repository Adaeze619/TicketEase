using System.ComponentModel.DataAnnotations.Schema;
using TicketEase.Domain.Enums;

namespace TicketEase.Domain.Entities
{
    public class Project : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string DepartmentName { get; set; }
        public Status Status { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public string BoardId { get; set; }
        [ForeignKey("BoardId")]
        public Board Board { get; set; }
    }
}
