namespace InternCapstone.Entity
{
    public class SubDivision
    {
        public int SubDivisionId { get; set; }
        public string? SubDivisionName { get; set; }
        public List<User> Users { get; set; } = new List<User>(); // Her bir alt birimde bir veya birden fazla kullanıcı olabilir.
        public Department Department { get; set; } = null!;// Her bir alt departmanın bir üst departmanı olabilir.
    }
}