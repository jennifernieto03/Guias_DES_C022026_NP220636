using Gestion_Vehiculos.Models;
using Microsoft.AspNetCore.Identity;

namespace Gestion_Vehiculos.Data
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<Usuario>>();

            // Crear los roles si no existen en AspNetRoles
            string[] roles = { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Crear un usuario Administrador por defecto
            var adminEmail = "admin@udb.edu.sv";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new Usuario
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123*");
                if (result.Succeeded)
                {
                    // Asignar el rol Admin en AspNetUserRoles
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}