using Microsoft.AspNetCore.Identity;

namespace InternCapstone.Models
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }
    }
}