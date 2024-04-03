using InternCapstone.Entity;
using Microsoft.AspNetCore.Identity;

namespace InternCapstone.Models
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }
        public List<SubDivision> SubDivisions { get; set; } = new List<SubDivision>(); // Kullanıcının bir veya birden fazla alt birimi olabilir.
    }
}