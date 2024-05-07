using System.ComponentModel.DataAnnotations;

namespace InternCapstone.ViewModels.Demand
{
    public class DemandViewModel
    {
        [Required]
        [MaxLength(250)]
        public string Text { get; set; } = string.Empty;
    }
}
