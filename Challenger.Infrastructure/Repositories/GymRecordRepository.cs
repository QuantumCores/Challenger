using Challenger.Domain.Contracts;
using Challenger.Domain.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Challenger.Infrastructure.Repositories
{
    public class GymRecordRepository : IGymRecordRepository
    {
        private readonly ChallengerContext _context;

        public GymRecordRepository(ChallengerContext context)
        {
            _context = context;
        }

        public void Add(GymRecord record)
        {
            _context.GymRecords.Add(record);
        }

        public ValueTask<GymRecord> Get(long id)
        {
            return _context.GymRecords.FindAsync(id);
        }

        public Task<List<GymRecord>> GetAll()
        {
            return _context.GymRecords.Include(x => x.User).ToListAsync();
        }

        public void Remove(GymRecord record)
        {
            _context.GymRecords.Remove(record);
        }

        public Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }

        public async Task Update(GymRecord record)
        {
            var dbRecord = await _context.GymRecords.FindAsync(record.Id);
            _context.Entry(dbRecord).CurrentValues.SetValues(record);
        }
    }
}
