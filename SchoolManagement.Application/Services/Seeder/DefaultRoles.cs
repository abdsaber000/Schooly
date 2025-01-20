using Microsoft.AspNetCore.Identity;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Infrastructure.Seeder;

public static class DefaultRoles
{
    public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
    {
        if (!roleManager.Roles.Any())
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            await roleManager.CreateAsync(new IdentityRole(Roles.Teacher));
            await roleManager.CreateAsync(new IdentityRole(Roles.Student));   
        }
    }
}