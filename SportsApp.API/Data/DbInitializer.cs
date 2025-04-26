using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportsApp.API.Models;
using System;
using System.Threading.Tasks;

namespace SportsApp.API.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var context = serviceProvider.GetRequiredService<SportsAppDbContext>();

            // Ensure the database is created and migrations are applied
            await context.Database.MigrateAsync();

            // Seed roles
            string[] roles = new[] { "Admin", "Player", "Kinesiologist" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }

            // Seed default admin user
            var adminEmail = "admin@sportsapp.com";
            var existingAdmin = await userManager.FindByEmailAsync(adminEmail);

            if (existingAdmin == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "Admin User",
                    Level = 10,
                    Bio = "Administrador",
                    Role = "Admin"
                };

                var result = await userManager.CreateAsync(adminUser, "Admin1234!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Seed Facility Types
            await SeedFacilityTypes(context);
        }

        private static async Task SeedFacilityTypes(SportsAppDbContext context)
        {
            var predefinedTypes = new List<string> { "Court", "FootballField", "Kinesiologia" };

            foreach (var typeName in predefinedTypes)
            {
                if (!context.FacilityTypes.Any(ft => ft.Name == typeName))
                {
                    context.FacilityTypes.Add(new FacilityType
                    {
                        Id = Guid.NewGuid(),
                        Name = typeName
                    });
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
