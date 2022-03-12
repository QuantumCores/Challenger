using Challenger.Domain.DbModels;

namespace Challenger.Domain.Contracts.Repositories
{
    public interface IDiaryRecordRepository
    {
        Task<DiaryRecord?> Get(long id);

        Task<List<DiaryRecord>> GetAll();

        Task<DiaryRecord?> GetByDate(long userId, DateTime dateOnly);

        Task<List<DiaryRecord>> GetAllForUser(long userId, DateTime startDate, DateTime endDate);

        void Add(DiaryRecord record);

        Task Update(DiaryRecord record);

        void Remove(DiaryRecord record);

        Task SaveChanges();
    }
}
