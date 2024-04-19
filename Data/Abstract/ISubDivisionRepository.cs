using InternCapstone.Entity;

namespace InternCapstone.Data.Abstract
{
    public interface ISubDivisionRepository
    {
        IQueryable<SubDivision> SubDivisions { get; }
        Task<int> GetSubDivisionIdByNameAsync(string? name);
        Task<List<string?>> GetSubDivisionNamesByDepartmentNameAsync(string department);
    }
}