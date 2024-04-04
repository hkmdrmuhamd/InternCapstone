using InternCapstone.Data.Abstract;
using InternCapstone.Entity;

namespace InternCapstone.Data.Concrete.EfCore
{
    public class EfDepartmentRepository : IDepartmentRepository
    {
        private DatabaseContext _context;
        public EfDepartmentRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IQueryable<Department> Departments => _context.Departments;
    }
}