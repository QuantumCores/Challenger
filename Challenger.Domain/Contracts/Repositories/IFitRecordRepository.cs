using Challenger.Domain.DbModels;

namespace Challenger.Domain.Contracts.Repositories
{
    public interface IFitRecordRepository
    {
        ValueTask<FitRecord> Get(long id);

        Task<List<FitRecord>> GetAll();

        Task<List<FitRecord>> GetAllByTimeRange(DateTime startDate, DateTime endDate);

        Task<List<FitRecord>> GetAllForUser(long userId);

        void Add(FitRecord record);

        Task Update(FitRecord record);

        void Remove(FitRecord record);

        Task SaveChanges();
    }
}
