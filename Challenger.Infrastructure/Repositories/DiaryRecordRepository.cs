using Challenger.Domain.Contracts.Repositories;
using Challenger.Domain.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenger.Infrastructure.Repositories
{
    public class DiaryRecordRepository : IDiaryRecordRepository
    {
        private readonly ChallengerFoodContext _context;

        public DiaryRecordRepository(ChallengerFoodContext context)
        {
            _context = context;
        }

        public void Add(DiaryRecord record)
        {
            _context.DiaryRecords.Add(record);
        }

        public Task<DiaryRecord?> Get(long id)
        {
            return _context.DiaryRecords
                //.Include(x => x.User)
                .Include(x => x.MealRecords)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<DiaryRecord>> GetAll()
        {
            return _context.DiaryRecords
                //.Include(x => x.User)
                .Include(x => x.MealRecords)
                .ToListAsync();
        }

        public Task<DiaryRecord?> GetByDate(long userId, DateTime dateOnly)
        {
            return _context.DiaryRecords.SingleOrDefaultAsync(x => x.UserId == userId && x.DiaryDate == dateOnly);
        }

        public Task<List<DiaryRecord>> GetAllForUser(long userId, DateTime startDate, DateTime endDate)
        {
            return _context.DiaryRecords
                //.Include(x => x.User)
                .Include(x => x.MealRecords)
                    .ThenInclude(x => x.MealProducts)
                    .ThenInclude(x => x.Product)
                .Include(x => x.MealRecords)
                    .ThenInclude(x => x.FastRecords)
                .Include(x => x.MealRecords)
                    .ThenInclude(x => x.MealDishes)
                    .ThenInclude(x => x.Dish)
                .Where(x => x.UserId == userId && x.DiaryDate >= startDate && x.DiaryDate <= endDate)
                .ToListAsync();
        }

        public void Remove(DiaryRecord record)
        {
            _context.DiaryRecords.Remove(record);
        }

        public Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }

        public async Task Update(DiaryRecord record)
        {
            var dbRecord = await _context.DiaryRecords.FindAsync(record.Id);
            _context.Entry(dbRecord).CurrentValues.SetValues(record);
        }
    }
}
