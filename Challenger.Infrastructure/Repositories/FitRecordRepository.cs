using Challenger.Domain.Contracts.Repositories;
using Challenger.Domain.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenger.Infrastructure.Repositories
{
    public class FitRecordRepository : IFitRecordRepository
    {
        private readonly ChallengerContext _context;

        public FitRecordRepository(ChallengerContext context)
        {
            _context = context;
        }

        public void Add(FitRecord record)
        {
            _context.FitRecords.Add(record);
        }

        public ValueTask<FitRecord> Get(long id)
        {
            return _context.FitRecords.FindAsync(id);
        }

        public Task<List<FitRecord>> GetAll()
        {
            return _context.FitRecords.Include(x => x.User).ToListAsync();
        }

        public Task<List<FitRecord>> GetAllByTimeRange(DateTime startDate, DateTime endDate)
        {
            var records = (IQueryable<FitRecord>)_context.FitRecords;
            if (startDate != default(DateTime))
            {
                records = records.Where(x => x.RecordDate >= startDate);
            }

            if(endDate != default(DateTime))
            {
                records = records.Where(y => y.RecordDate <= endDate);
            }

            return records.Include(x => x.User).ToListAsync();
        }

        public Task<List<FitRecord>> GetAllForUser(long userId)
        {
            return _context.FitRecords.Where(x => x.UserId == userId).ToListAsync();
        }

        public void Remove(FitRecord record)
        {
            _context.FitRecords.Remove(record);
        }

        public Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }

        public async Task Update(FitRecord record)
        {
            var dbRecord = await _context.FitRecords.FindAsync(record.Id);
            _context.Entry(dbRecord).CurrentValues.SetValues(record);
        }
    }
}
