using InternCapstone.Entity;

namespace InternCapstone.Data.Abstract
{
    public interface IDemandRepository
    {
        IQueryable<Demand> Demands { get; }

        Task<string?> GetDepartmentNameByUserNameAsync(string? userName);
        Task<string?> GetTextByUserNameAsync(string? userName);
        Task<string?> GetStatusByUserNameAsync(string? userName);
        Task<int?> GetIdByUserName(string? userName);
    }
}