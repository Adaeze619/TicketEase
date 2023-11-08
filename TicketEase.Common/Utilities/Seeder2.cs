using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TicketEase.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace TicketEase.Common.Utilities
{
    public class Seeder2
    {
        private static string adminRoleId;
        private static string superAdminRoleId;
        private static string userRoleId;




		public static void SeedRoles(IServiceProvider serviceProvider)
		{
			var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
			if (!roleManager.RoleExistsAsync("SuperAdmin").Result)
			{
				var role = new IdentityRole("SuperAdmin");
				roleManager.CreateAsync(role).Wait();
			}

			if (!roleManager.RoleExistsAsync("Manager").Result)
			{
				var role = new IdentityRole("Manager");
				roleManager.CreateAsync(role).Wait();
			}

			if (!roleManager.RoleExistsAsync("User").Result)
			{
				var role = new IdentityRole("User");
				roleManager.CreateAsync(role).Wait();
			}
		}

		private static void SeedSuperAdminUser(ModelBuilder builder)
        {
            var superAdminId = Guid.NewGuid().ToString();
            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@library.com",
                Email = "superadmin@library.com",
                NormalizedEmail = "superadmin@library.com".ToUpper(),
                NormalizedUserName = "superadmin@library.com".ToUpper(),
                Id = superAdminId
            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "SuperAdmin@123");
            builder.Entity<IdentityUser>().HasData(superAdminUser);

            // Use the stored role IDs
            var superAdminRoles = new List<IdentityUserRole<string>>
        {
            new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = superAdminId
            },
            new IdentityUserRole<string>
            {
                RoleId = superAdminRoleId,
                UserId = superAdminId
            },
            new IdentityUserRole<string>
            {
                RoleId = userRoleId,
                UserId = superAdminId
            }
        };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}
