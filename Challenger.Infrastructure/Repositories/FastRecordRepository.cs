using Challenger.Domain.Contracts.Repositories;
using Challenger.Domain.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Challenger.Infrastructure.Repositories
{
    public class FastRecordRepository: IFastRecordRepository
    {
        private readonly ChallengerFoodContext _context;

        public FastRecordRepository(ChallengerFoodContext context)
        {
            _context = context;
        }

        public void Add(FastRecord record)
        {
            _context.FastRecords.Add(record);
        }

        public ValueTask<FastRecord> Get(long id)
        {
            return _context.FastRecords.FindAsync(id);
        }

        public Task<List<FastRecord>> GetAll()
        {
            return _context.FastRecords
                //.Include(x => x.Dish)
                .ToListAsync();
        }

        //public Task<List<FastRecord>> GetAllByDate(DateTime date)
        //{
        //    return _context.FastRecords
        //        .Include(x => x.Dishes)
        //        .Include(x => x.Products)                
        //        .ToListAsync();
        //}

        public void Remove(FastRecord record)
        {
            _context.FastRecords.Remove(record);
        }

        public Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }

        public async Task Update(FastRecord record)
        {
            var dbRecord = await _context.FastRecords.FindAsync(record.Id);
            _context.Entry(dbRecord).CurrentValues.SetValues(record);
        }
    }
}
