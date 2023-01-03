using Challenger.Domain.DbModels;
using System.Collections.Generic;
using System.Threading.Tasks;

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
