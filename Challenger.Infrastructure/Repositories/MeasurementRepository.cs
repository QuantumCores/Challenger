using Challenger.Domain.Contracts.Repositories;
using Challenger.Domain.DbModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenger.Infrastructure.Repositories
{
    public class MeasurementRepository : IMeasurementRepository
    {
        private readonly ChallengerContext _context;

        public MeasurementRepository(ChallengerContext context)
        {
            _context = context;
        }

        public void Add(Measurement record)
        {
            _context.Measurements.Add(record);
        }

        public ValueTask<Measurement> Get(long id)
        {
            return _context.Measurements.FindAsync(id);
        }

        public Task<List<Measurement>> GetAll()
        {
            return _context.Measurements.Include(x => x.User).ToListAsync();
        }

        public Task<List<Measurement>> GetAllForUser(long userId)
        {
            return _context.Measurements.Where(x => x.UserId == userId).ToListAsync();
        }

        public void Remove(Measurement record)
        {
            _context.Measurements.Remove(record);
        }

        public Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }

        public async Task Update(Measurement record)
        {
            var dbRecord = await _context.Measurements.FindAsync(record.Id);
            _context.Entry(dbRecord).CurrentValues.SetValues(record);
        }
    }
}
