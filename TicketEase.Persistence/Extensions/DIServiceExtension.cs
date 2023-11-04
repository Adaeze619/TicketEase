using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketEase.Persistence.DbContext;

namespace TicketEase.Persistence.Extensions
{
    public static class DIServiceExtension
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<TicketEaseDbContext>(opt => opt.
           UseSqlServer(configuration.GetConnectionString("TMSconn")));
        }
    }
}
