using Challenger.Domain.DbModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Challenger.Domain.Contracts.Repositories
{
    public interface IFitRecordRepository
    {
        ValueTask<FitRecord> Get(long id);

        Task<List<FitRecord>> GetAll();

        Task<List<FitRecord>> GetAllByTimeRange(DateTime startDate, DateTime endDate, Guid[] users);

        Task<List<FitRecord>> GetAllForUser(long userId);

        void Add(FitRecord record);

        Task Update(FitRecord record);

        void Remove(FitRecord record);

        Task SaveChanges();
    }
}
