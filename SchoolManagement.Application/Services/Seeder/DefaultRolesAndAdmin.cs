using Microsoft.AspNetCore.Identity;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Services.Seeder;

public static class DefaultRolesAndAdmin
{
    public static async Task SeedAsync(
        RoleManager<IdentityRole> roleManager,
        UserManager<ApplicationUser> userManager)
    {
        // Seed Roles
        if (!roleManager.Roles.Any())
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            await roleManager.CreateAsync(new IdentityRole(Roles.Teacher));
            await roleManager.CreateAsync(new IdentityRole(Roles.Student)); 
        }

        // Seed Admin User
        string adminEmail = "admin@gmail.com";
        string adminPassword = "123456";

        var existingAdmin = await userManager.FindByEmailAsync(adminEmail);
        if (existingAdmin == null)
        {
            var admin = new Admin
            {
                UserName = adminEmail,
                Email = adminEmail,
                Name = "Admin",
                PhoneNumber = adminEmail,
            };

            var result = await userManager.CreateAsync(admin, adminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, Roles.Admin);
            }
        }
    }
}