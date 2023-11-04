using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TicketEase.Domain.Entities;

namespace TicketEase.Persistence.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                .UseSqlServer("Server=INNOCHU;Database=ticket;Trusted_Connection=true;TrustServerCertificate = true");
        }

        public DbSet<User> Users { get; set; }             // => Set<User>();
      

    }
}
