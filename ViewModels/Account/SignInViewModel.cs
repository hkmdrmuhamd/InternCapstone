using System.ComponentModel.DataAnnotations;

namespace InternCapstone.ViewModels.Account
{
    public class SignInViewModel
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public bool RememberMe { get; set; } = true;
    }
}