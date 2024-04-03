namespace InternCapstone.Entity
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public List<SubDivision> SubDivision { get; set; } = new List<SubDivision>(); // Her bir departmanın altında bir veya birden fazla alt birim olabilir.
    }
}