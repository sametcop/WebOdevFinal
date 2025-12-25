using Microsoft.AspNetCore.Identity;
using UrunYonetimSistemi.Models;

namespace UrunYonetimSistemi.Data
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider service)
        {
            // Servisleri al
            var userManager = service.GetService<UserManager<AppUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();

            // Rolleri oluştur
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            await roleManager.CreateAsync(new IdentityRole("Member"));

            // Admin kullanıcısını oluştur
            var adminUser = await userManager.FindByEmailAsync("samet@admin.com");
            if (adminUser == null)
            {
                adminUser = new AppUser
                {
                    UserName = "samet",
                    Email = "samet@admin.com",
                    FirstName = "Samet",
                    LastName = "Çöp",
                    EmailConfirmed = true,
                };

                // Şifre: fener1907samsun
                var result = await userManager.CreateAsync(adminUser, "fener1907samsun");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
