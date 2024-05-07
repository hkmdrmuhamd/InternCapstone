using InternCapstone.Data.Abstract;
using InternCapstone.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<string?> GetDepartmentNameByUserNameAsync(string? userName)
        {
            return await _context.Users.Where(b => b.UserName == userName).Select(b => b.Department).FirstOrDefaultAsync();
        }
    }
}