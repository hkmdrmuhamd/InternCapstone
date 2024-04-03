using InternCapstone.Entity;
using Microsoft.AspNetCore.Identity;

namespace InternCapstone.Models
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }
        public int SubDivisionId { get; set; }
        public List<SubDivision> SubDivisions { get; set; } = new List<SubDivision>(); // Kullanıcının çalıştığı bir veya birden fazla alt birim olabilir.
    }
}