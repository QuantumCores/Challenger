using Challenger.Domain.DbModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Challenger.Domain.Contracts.Repositories
{
    public interface IUserChallengeRepository
    {
        ValueTask<UserChallenge> Get(long id);

        Task<List<UserChallenge>> GetAll();

        Task<List<UserChallenge>> GetAllForUser(Guid userId);

        void Add(UserChallenge record);

        Task Update(UserChallenge record);

        void Remove(UserChallenge record);

        Task SaveChanges();
    }
}
