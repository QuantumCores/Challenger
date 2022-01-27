using Challenger.Domain.DbModels;

namespace Challenger.Domain.Contracts
{
    public interface IFitRecordRepository
    {
        ValueTask<FitRecord> Get(long id);

        Task<List<FitRecord>> GetAll();

        void Add(FitRecord record);

        Task Update(FitRecord record);

        void Remove(FitRecord record);

        Task SaveChanges();
    }
}
