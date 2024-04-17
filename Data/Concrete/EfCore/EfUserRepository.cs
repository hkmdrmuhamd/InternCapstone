using InternCapstone.Data.Abstract;
using InternCapstone.Models;

namespace InternCapstone.Data.Concrete.EfCore
{
    public class EfUserRepository : IUserRepository
    {
        private DatabaseContext _context;
        public EfUserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IQueryable<AppUser> Users => _context.Users;

    }
}