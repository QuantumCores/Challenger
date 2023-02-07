using Challenger.Domain.Contracts.Repositories;
using Challenger.Domain.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenger.Infrastructure.Repositories
{
    public class ChallengeRepository : IChallengeRepository
    {
        private const int MaxResultCount = 10;
        private readonly ChallengerContext _context;

        public ChallengeRepository(ChallengerContext context)
        {
            _context = context;
        }

        public void Add(Challenge record)
        {
            _context.Challenges.Add(record);
        }

        public ValueTask<Challenge> Get(long id)
        {
            return _context.Challenges.FindAsync(id);
        }

        public Task<Challenge> GetWithAllData(long id)
        {
            return _context.Challenges.Include(x => x.User)
                                      .Include(x => x.Participants)
                                        //.ThenInclude(x => x.User)
                                      .Where(x => x.Id == id)
                                      .SingleAsync();
        }

        public Task<List<Challenge>> GetByName(Guid userId, string name)
        {
            return _context.Challenges.Include(x => x.User)
                                      .Include(x => x.Participants)
                                        .ThenInclude(x => x.User)
                                      .Where(x => x.Name.Contains(name) && x.CreatorId != userId)
                                      .ToListAsync();
        }

        public Task<List<Challenge>> GetAll()
        {
            return _context.Challenges.Include(x => x.User).ToListAsync();
        }

        public Task<List<Challenge>> GetAllForUser(Guid userId)
        {
            return _context.Challenges.Where(x => x.CreatorId == userId).ToListAsync();
        }

        public Task<List<Challenge>> GetWithCustomFormulas()
        {
            return Active().Where(x =>
                                !x.IsUsingFitDefaultFormula ||
                                !x.IsUsingGymDefaultFormula ||
                                !x.IsUsingMeasurementDefaultFormula)
                           .ToListAsync();
        }

        public Task<List<Challenge>> Find(string search)
        {
            return _context.Challenges
                .Where(x => x.Name.Contains(search))
                .Take(MaxResultCount)
                .ToListAsync();
        }

        public void Remove(Challenge record)
        {
            _context.Challenges.Remove(record);
        }

        public Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }

        public async Task Update(Challenge record)
        {
            var dbRecord = await _context.Challenges.FindAsync(record.Id);
            _context.Entry(dbRecord).CurrentValues.SetValues(record);
        }

        private IQueryable<Challenge> Active()
        {
            var now = DateTime.UtcNow.Date;
            return this._context.Challenges.Where(x => x.StartDate.Date <= now && x.EndDate.Date >= now);
        }
    }
}
