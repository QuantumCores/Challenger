using Challenger.Domain.DbModels;

namespace Challenger.Domain.Contracts.Repositories
{
    public interface IIngridientRepository
    {
        ValueTask<Ingridient> Get(long id);

        Task<List<Ingridient>> GetAll();

        void Add(Ingridient record);

        Task Update(Ingridient record);

        void Remove(Ingridient record);

        Task SaveChanges();
    }
}
