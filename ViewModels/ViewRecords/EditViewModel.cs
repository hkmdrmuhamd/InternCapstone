using System.ComponentModel.DataAnnotations;

namespace InternCapstone.ViewModels.ViewRecords
{
    public class EditViewModel
    {
        public string? Id { get; set; }
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
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