using System.Linq.Expressions;
using TicketEase.Application.Interfaces.Repositories;
using TicketEase.Domain.Entities;
using TicketEase.Domain.Enums;
using TicketEase.Persistence.Context;

namespace TicketEase.Persistence.Repositories
{
    public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
	{
		public TicketRepository(TicketEaseDbContext ticketEaseDbContext) : base(ticketEaseDbContext) { }

		public void AddTicket(Ticket ticket) => Add(ticket);

		public void DeleteTicket(Ticket ticket) => Delete(ticket);

		public List<Ticket> FindTicket(Expression<Func<Ticket, bool>> condition)
		{
			return Find(condition);
		}

		public Ticket GetTicketById(string id)
		{
			return GetById(id);
		}

		public async Task<List<Ticket>> GetTicketByProjectId(Expression<Func<Ticket, bool>> condition)
		{
			return Find(condition);
		}

		public async Task<List<Ticket>> GetTicketByUserId(Expression<Func<Ticket, bool>> condition)
		{
			return Find(condition);
		}

		public List<Ticket> GetTickets()
		{
			return GetAll();
		}

<<<<<<< HEAD
        public void UpdateTicket(Ticket ticket)
		{
			Update(ticket);
		}
=======
		public void UpdateTicket(Ticket ticket) => Update(ticket);
>>>>>>> 2eb9a4ba010210a0e8797fb925d810e9d9808ef9
	}
}
