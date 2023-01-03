using Challenger.Domain.DbModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Challenger.Domain.Contracts.Repositories
{
    public interface IFastRecordRepository
    {
        ValueTask<FastRecord> Get(long id);

        Task<List<FastRecord>> GetAll();

        //Task<List<FastRecord>> GetAllByDate(DateTime date);        

        void Add(FastRecord record);

        Task Update(FastRecord record);

        void Remove(FastRecord record);

        Task SaveChanges();
    }
}
