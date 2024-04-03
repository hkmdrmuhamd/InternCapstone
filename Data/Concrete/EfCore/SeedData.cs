using InternCapstone.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InternCapstone.Data.Concrete.EfCore
{
    public static class SeedData
    {
        private const string adminUser = "admin";
        private const string adminPassword = "Admin_123";

        public static async void TestUser(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<DatabaseContext>();
            if (context.Database.GetAppliedMigrations().Any())
            {
                context.Database.Migrate();
            }

            var userManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            var user = await userManager.FindByNameAsync(adminUser);

            if (user == null)
            {
                user = new AppUser
                {
                    UserName = adminUser,
                    FullName = "Admin",
                    Email = "admin@mhkmdr.com",
                    PhoneNumber = "1234567890"
                };
                await userManager.CreateAsync(user, adminPassword);
            }
        }
    }
}
