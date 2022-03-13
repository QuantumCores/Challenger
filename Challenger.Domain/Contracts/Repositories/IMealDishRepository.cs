using Challenger.Domain.DbModels;

namespace Challenger.Domain.Contracts.Repositories
{
    public interface IMealDishRepository
    {
        ValueTask<MealDish> Get(long id);

        Task<List<MealDish>> GetAll();

        //Task<List<MealDish>> GetAllByDate(DateTime date);        

        void Add(MealDish record);

        Task Update(MealDish record);

        void Remove(MealDish record);

        Task SaveChanges();
    }
}
