using Challenger.Domain.DbModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Challenger.Domain.Contracts.Repositories
{
    public interface IProductRepository
    {
        ValueTask<Product> Get(long id);

        Task<List<Product>> Find(string search);

        Task<List<Product>> GetAll();

        void Add(Product record);

        Task Update(Product record);

        void Remove(Product record);

        Task SaveChanges();
    }
}
