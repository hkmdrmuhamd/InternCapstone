using System.ComponentModel.DataAnnotations;
using InternCapstone.Entity;

namespace InternCapstone.ViewModels.Department
{
    public class DepartmentViewModel
    {
        public string? Id { get; set; }
        public string? DepartmentName { get; set; }
        public List<SubDivision> SubDivisions { get; set; } = new List<SubDivision>();
    }

}