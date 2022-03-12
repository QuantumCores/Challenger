using Challenger.Domain.DbModels;

namespace Challenger.Domain.Contracts.Repositories
{
    public interface IMealRecordRepository
    {
        ValueTask<MealRecord> Get(long id);

        Task<List<MealRecord>> GetAll();

        //Task<List<MealRecord>> GetAllByDate(DateTime date);        

        void Add(MealRecord record);

        Task Update(MealRecord record);

        void Remove(MealRecord record);

        Task SaveChanges();
    }
}
