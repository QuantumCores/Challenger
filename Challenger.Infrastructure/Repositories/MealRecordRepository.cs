using Challenger.Domain.Contracts.Repositories;
using Challenger.Domain.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Challenger.Infrastructure.Repositories
{
    public class MealRecordRepository : IMealRecordRepository
    {
        private readonly ChallengerFoodContext _context;

        public MealRecordRepository(ChallengerFoodContext context)
        {
            _context = context;
        }

        public void Add(MealRecord record)
        {
            _context.MealRecords.Add(record);
        }

        public ValueTask<MealRecord> Get(long id)
        {
            return _context.MealRecords.FindAsync(id);
        }

        public Task<List<MealRecord>> GetAll()
        {
            return _context.MealRecords
                .Include(x => x.MealDishes)
                .Include(x => x.MealProducts)
                .Include(x => x.FastRecords)
                .ToListAsync();
        }

        //public Task<List<MealRecord>> GetAllByDate(DateTime date)
        //{
        //    return _context.MealRecords
        //        .Include(x => x.Dishes)
        //        .Include(x => x.Products)                
        //        .ToListAsync();
        //}

        public void Remove(MealRecord record)
        {
            _context.MealRecords.Remove(record);
        }

        public Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }

        public async Task Update(MealRecord record)
        {
            var dbRecord = await _context.MealRecords.FindAsync(record.Id);
            _context.Entry(dbRecord).CurrentValues.SetValues(record);
        }
    }
}
