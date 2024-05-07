using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InternCapstone.ViewModels.Demand
{
    public class DemandViewModel
    {
        [Required]
        [MaxLength(250)]
        public string Text { get; set; } = string.Empty;
        public string? Status { get; set; }
    }
}
