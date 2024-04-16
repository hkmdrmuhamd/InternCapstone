using System.ComponentModel.DataAnnotations;
using InternCapstone.Entity;
using InternCapstone.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InternCapstone.ViewModels
{
    public class SignUpViewModel
    {
        [Required]
        public string? FullName { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Parolalar eşleşmiyor.")]
        public string ConfirmPassword { get; set; } = string.Empty;
        public string? SelectedRole { get; set; }
        [Required]
        public List<SelectListItem>? Departments { get; set; }
        public string? DepartmentId { get; set; }
        public string? SubDivision { get; set; }
        public List<SubDivision> SubDivisions { get; set; } = new List<SubDivision>();
    }
}