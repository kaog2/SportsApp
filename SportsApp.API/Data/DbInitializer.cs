using Microsoft.AspNetCore.Identity;
using SportsApp.API.Models;
using System;
using System.Threading.Tasks;

namespace SportsApp.API.Data
{
    public class DbInitializer
    {
        public static async Task SeedRolesAndAdmin(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            var adminRole = "Admin";

            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(adminRole));
            }

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
                    Bio = "Administrador"
                };

                var result = await userManager.CreateAsync(adminUser, "Admin1234!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, adminRole);
                }
            }
        }

        public static async Task SeedFacilityTypes(SportsAppDbContext context)
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
