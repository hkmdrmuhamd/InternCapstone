using InternCapstone.Entity;
using InternCapstone.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InternCapstone.Data.Concrete.EfCore
{
    public class DatabaseContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Department> Departments => Set<Department>();
        public DbSet<SubDivision> SubDivisions => Set<SubDivision>();
    }
}