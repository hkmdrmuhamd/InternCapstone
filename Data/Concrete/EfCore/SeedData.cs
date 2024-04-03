using InternCapstone.Entity;
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

            var userAdmin = await userManager.FindByNameAsync(adminUser);

            if (userAdmin == null)
            {
                userAdmin = new AppUser
                {
                    UserName = adminUser,
                    FullName = "Admin",
                    Email = "admin@mhkmdr.com",
                    PhoneNumber = "1234567890",
                };
                await userManager.CreateAsync(userAdmin, adminPassword);
            }

            if (!context.Departments.Any())
            {
                context.Departments.AddRange(
                    new Department
                    {
                        DepartmentName = "Bilgi İşlem Departmanı",
                        SubDivisions = new List<SubDivision>
                        {
                            new SubDivision {
                                 SubDivisionName = "Yazılım Geliştirme"
                            },
                            new SubDivision {
                                SubDivisionName = "Sistem Yönetimi"
                            },
                            new SubDivision {
                                SubDivisionName = "Network Yönetimi"
                            }
                        }
                    },
                    new Department
                    {
                        DepartmentName = "İnsan Kaynakları Departmanı",
                        SubDivisions = new List<SubDivision>
                        {
                            new SubDivision {
                                SubDivisionName = "İşe Alım"
                            },
                            new SubDivision {
                                SubDivisionName = "İşten Çıkarma"
                            }
                        }
                    },
                    new Department
                    {
                        DepartmentName = "Maliye Departmanı",
                        SubDivisions = new List<SubDivision>
                        {
                            new SubDivision {
                                SubDivisionName = "Muhasebe"
                            },
                            new SubDivision {
                                SubDivisionName = "Finans"
                            }
                        }
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
