using InternCapstone.Entity;

namespace InternCapstone.Data.Abstract
{
    public interface IDepartmentRepository
    {
        IQueryable<Department> Departments { get; }
    }
}