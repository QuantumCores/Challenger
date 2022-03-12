using Challenger.Domain.DbModels;

namespace Challenger.Domain.Contracts.Repositories
{
    public interface IMealProductRepository
    {
        ValueTask<MealProduct> Get(long id);

        Task<List<MealProduct>> GetAll();

        //Task<List<MealProduct>> GetAllByDate(DateTime date);        

        void Add(MealProduct record);

        Task Update(MealProduct record);

        void Remove(MealProduct record);

        Task SaveChanges();
    }
}
