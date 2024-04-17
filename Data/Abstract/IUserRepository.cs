using InternCapstone.Models;

namespace InternCapstone.Data.Abstract
{
    public interface IUserRepository
    {
        IQueryable<AppUser> Users { get; }
    }
}