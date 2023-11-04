using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketEase.Domain.Entities;

namespace TicketEase.Persistence.DbContext
{
    public class TicketEaseDbContext : IdentityDbContext
    {
        public TicketEaseDbContext(DbContextOptions options)
            : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

    }
}
