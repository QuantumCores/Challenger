using Challenger.Domain.DbModels;

namespace Challenger.Domain.Contracts.Repositories
{
    public interface IGymRecordRepository
    {
        ValueTask<GymRecord> Get(long id);

        Task<List<GymRecord>> GetAll();

        Task<List<GymRecord>> GetAllByTimeRange(DateTime startDate, DateTime endDate);

        Task<List<GymRecord>> GetAllForUser(long userId);

        void Add(GymRecord record);

        Task Update(GymRecord record);

        void Remove(GymRecord record);

        Task SaveChanges();
        
        Task<List<GymRecord>> Dynamic();
    }
}
