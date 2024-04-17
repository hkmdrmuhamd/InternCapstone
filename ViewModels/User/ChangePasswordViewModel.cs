using System.ComponentModel.DataAnnotations;

namespace InternCapstone.ViewModels.User
{
    public class ChangePasswordViewModel
    {
        public string? Id { get; set; }
        [DataType(DataType.Password)]
        public string? OldPassword { get; set; }
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Parolalar eşleşmiyor.")]
        public string? ConfirmPassword { get; set; }
        public IList<string>? SelectedRoles { get; set; }
    }

}