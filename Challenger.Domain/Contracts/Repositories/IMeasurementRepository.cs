using Challenger.Domain.DbModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Challenger.Domain.Contracts.Repositories
{
    public interface IMeasurementRepository
    {
        ValueTask<Measurement> Get(long id);

        Task<List<Measurement>> GetAll();

        Task<List<Measurement>> GetAllForUser(long userId);

        void Add(Measurement record);

        Task Update(Measurement record);

        void Remove(Measurement record);

        Task SaveChanges();
    }
}
