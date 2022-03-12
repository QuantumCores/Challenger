using Challenger.Domain.Contracts.Repositories;
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

        public Task<List<GymRecord>> GetAllByTimeRange(DateTime startDate, DateTime endDate)
        {
            var records = (IQueryable<GymRecord>)_context.GymRecords;
            if (startDate != default(DateTime))
            {
                records = records.Where(x => x.RecordDate >= startDate);
            }

            if (endDate != default(DateTime))
            {
                records = records.Where(y => y.RecordDate <= endDate);
            }

            return records.Include(x => x.User).ToListAsync();
        }

        public Task<List<GymRecord>> GetAllForUser(long userId)
        {
            return _context.GymRecords.Where(x => x.UserId == userId).ToListAsync();
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
