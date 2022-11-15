using Challenger.Domain.DbModels;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Challenger.Domain.Contracts.Repositories
{
    public interface IUserRepository
    {
        ValueTask<EntityEntry<User>> Add(User user);

        ValueTask<User> Get(long id);

        Task<User> GetByCorrelationId(string v);

        Task<long> GetIdByCorrelationId(string email);

        Task<List<User>> GetAll();

        void Remove(User user);

        Task SaveChanges();

        Task Update(User user);
    }
}