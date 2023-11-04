using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketEase.Persistence.DbContext;

namespace TicketEase.Persistence.Extension
{
    public static class ConfigServices
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<TicketEaseDbContext>(opt => opt.
           UseSqlServer(configuration.GetConnectionString("TMSconn")));
        }
    }
}
