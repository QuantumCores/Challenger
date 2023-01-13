using Challenger.Domain.DbModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Challenger.Domain.Contracts.Repositories
{
    public interface IChallengeRepository
    {
        ValueTask<Challenge> Get(long id);

        Task<List<Challenge>> GetAll();

        Task<List<Challenge>> GetAllForUser(Guid userId);

        Task<List<Challenge>> Find(string search);

        void Add(Challenge record);

        Task Update(Challenge record);

        void Remove(Challenge record);

        Task SaveChanges();
    }
}
