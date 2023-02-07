using Challenger.Domain.Contracts.Repositories;
using Challenger.Domain.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenger.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ChallengerContext _context;

        public UserRepository(ChallengerContext context)
        {
            _context = context;
        }

        public ValueTask<EntityEntry<User>> Add(User user)
        {
            return _context.Users.AddAsync(user);
        }

        public ValueTask<User?> Get(long id)
        {
            return _context.Users.FindAsync(id);
        }

        public Task<User> GetByCorrelationId(Guid correlationId)
        {
            return _context.Users.SingleAsync(x => x.CorrelationId == correlationId);
        }

        public Task<User> GetByCorrelationIdNoTracking(Guid correlationId)
        {
            return _context.Users.AsNoTracking()
                                 .SingleAsync(x => x.CorrelationId == correlationId);
        }

        public Task<List<User>> GetManyByCorrelationId(Guid[] guids)
        {
            return _context.Users.Where(x => guids.Contains(x.CorrelationId))
                                 .ToListAsync();
        }

        public async Task<long> GetIdByCorrelationId(string correlationId)
        {
            var user = await _context.Users.SingleAsync(x => x.CorrelationId == Guid.Parse(correlationId));
            return user.Id;
        }

        public Task<List<User>> GetAll()
        {
            return _context.Users.ToListAsync();
        }

        public void Remove(User user)
        {
            _context.Users.Remove(user);
        }

        public Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }

        public async Task Update(User user)
        {
            var dbuser = await _context.Users.FindAsync(user.Id);
            _context.Entry(dbuser).CurrentValues.SetValues(user);
        }
    }
}
