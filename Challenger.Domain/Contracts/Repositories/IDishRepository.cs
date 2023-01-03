using Challenger.Domain.DbModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Challenger.Domain.Contracts.Repositories
{
    public interface IDishRepository
    {
        ValueTask<Dish> Get(long id);

        Task<List<Dish>> GetAll();

        Task<List<Dish>> GetAllForUser(long userId);

        Task<List<Dish>> Find(string search);

        void Add(Dish record);

        Task Update(Dish record);

        void Remove(Dish record);

        Task SaveChanges();
    }
}
