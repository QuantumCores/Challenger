using Challenger.Domain.DbModels;

namespace Challenger.Domain.Contracts
{
    public interface IUserRepository
    {
        void Add(User user);

        ValueTask<User> Get(long id);

        Task<long> GetIdByEmail(string email);

        Task<List<User>> GetAll();

        void Remove(User user);

        Task SaveChanges();

        Task Update(User user);
    }
}