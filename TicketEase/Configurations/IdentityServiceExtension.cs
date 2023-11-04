using Microsoft.AspNetCore.Identity;

namespace TicketEase.Configurations
{
    public static class IdentityServiceExtension
    {
        public static void IdentityConfiguration(this IServiceCollection services)
        {
            var builder = services.AddIdentity<Users, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 6;
                

            });
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            builder.AddEntityFrameworkStores <TicketEaseDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}
