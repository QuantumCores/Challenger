using Challenger.Domain.Contracts.Repositories;
using Challenger.Domain.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenger.Infrastructure.Repositories
{
    public class UserChallengeRepository : IUserChallengeRepository
    {
        private const int MaxResultCount = 10;
        private readonly ChallengerContext _context;

        public UserChallengeRepository(ChallengerContext context)
        {
            _context = context;
        }

        public void Add(UserChallenge record)
        {
            _context.UserChallenges.Add(record);
        }

        public ValueTask<UserChallenge> Get(long id)
        {
            return _context.UserChallenges.FindAsync(id);
        }

        public Task<List<UserChallenge>> GetAll()
        {
            return _context.UserChallenges.Include(x => x.User).ToListAsync();
        }

        public Task<List<UserChallenge>> GetAllForUser(Guid userId)
        {
            return _context.UserChallenges.Where(x => x.UserCorrelationId == userId)
                                          .Include(x => x.User)
                                          .Include(x => x.Challenge)
                                            .ThenInclude(x => x.Participants)
                                            .ThenInclude(x => x.User)
                                          .ToListAsync();
        }

        public Task<int> GetCountForUser(Guid userId)
        {
            return _context.UserChallenges.Where(x => x.UserCorrelationId == userId)
                                          .CountAsync();
        }

        public Task<List<UserChallenge>> GetForChallenge(long challengeId)
        {
            return _context.UserChallenges.Where(x => x.ChallengeId == challengeId)
                                          .ToListAsync();
        }

        public void Remove(UserChallenge record)
        {
            _context.UserChallenges.Remove(record);
        }

        public Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }

        public async Task Update(UserChallenge record)
        {
            var dbRecord = await _context.UserChallenges.FindAsync(record.Id);
            _context.Entry(dbRecord).CurrentValues.SetValues(record);
        }
    }
}
