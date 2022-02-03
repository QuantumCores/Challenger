using Challenger.Domain.DbModels;

namespace Challenger.Domain.Contracts
{
    public interface IGymRecordRepository
    {
        ValueTask<GymRecord> Get(long id);

        Task<List<GymRecord>> GetAll();

        Task<List<GymRecord>> GetAllForUser(long userId);

        void Add(GymRecord record);

        Task Update(GymRecord record);

        void Remove(GymRecord record);

        Task SaveChanges();
    }
}
