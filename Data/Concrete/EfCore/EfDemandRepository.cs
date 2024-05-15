using InternCapstone.Data.Abstract;
using InternCapstone.Entity;
using Microsoft.EntityFrameworkCore;

namespace InternCapstone.Data.Concrete.EfCore
{
    public class EfDemandRepository : IDemandRepository
    {
        private DatabaseContext _context;
        public EfDemandRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IQueryable<Demand> Demands => _context.Demands;

        public async Task<string?> GetDepartmentNameByUserNameAsync(string? userName)
        {
            return await _context.Demands.Where(b => b.UserName == userName).Select(b => b.DepartmentName).FirstOrDefaultAsync();
        }

        public async Task<string?> GetTextByUserNameAsync(string? userName)
        {
            return await _context.Demands.Where(b => b.UserName == userName).Select(b => b.Text).FirstOrDefaultAsync();
        }

        public async Task<string?> GetStatusByTextAsync(string? text)
        {
            return await _context.Demands.Where(b => b.Text == text).Select(b => b.Status).FirstOrDefaultAsync();
        }

        public async Task<int?> GetIdByUserName(string? userName)
        {
            return await _context.Demands.Where(b => b.UserName == userName).Select(b => b.Id).FirstOrDefaultAsync();
        }

        public async Task<int?> GetIdByText(string? text)
        {
            return await _context.Demands.Where(b => b.Text == text).Select(b => b.Id).FirstOrDefaultAsync();
        }
    }
}