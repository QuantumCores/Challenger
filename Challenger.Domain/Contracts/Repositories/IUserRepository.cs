using Challenger.Domain.DbModels;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Challenger.Domain.Contracts.Repositories
{
    public interface IUserRepository
    {
        ValueTask<EntityEntry<User>> Add(User user);

        ValueTask<User> Get(long id);

        Task<User> GetByCorrelationId(Guid guid);

        Task<List<User>> GetManyByCorrelationId(Guid[] guid);

        Task<long> GetIdByCorrelationId(string email);

        Task<List<User>> GetAll();

        void Remove(User user);

        Task SaveChanges();

        Task Update(User user);
    }
}