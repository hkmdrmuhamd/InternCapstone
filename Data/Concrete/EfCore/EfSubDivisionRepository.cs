using InternCapstone.Data.Abstract;
using InternCapstone.Entity;
using Microsoft.EntityFrameworkCore;

namespace InternCapstone.Data.Concrete.EfCore
{
    public class EfSubDivisionRepository : ISubDivisionRepository
    {
        private DatabaseContext _context;
        public EfSubDivisionRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IQueryable<SubDivision> SubDivisions => _context.SubDivisions;

        public async Task<int> GetSubDivisionIdByNameAsync(string? name)
        {
            return await _context.SubDivisions.Where(b => b.SubDivisionName == name).Select(b => b.SubDivisionId).FirstOrDefaultAsync();
        }
    }
}