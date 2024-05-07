using System.ComponentModel.DataAnnotations;

namespace InternCapstone.Entity
{
    public class Demand
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public string? Text { get; set; }
        public string? UserName { get; set; }
        public string? DepartmentName { get; set; }
        public string? Status { get; set; }
    }
}