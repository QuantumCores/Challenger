using Challenger.Domain.Contracts;
using Challenger.Domain.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Challenger.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ChallengerContext _context;

        public UserRepository(ChallengerContext context)
        {
            _context = context;
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
        }

        public ValueTask<User> Get(long id)
        {
            return _context.Users.FindAsync(id);
        }

        public async Task<long> GetIdByEmail(string email)
        {
            var user = await _context.Users.SingleAsync(x => x.Email == email);
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
