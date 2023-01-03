using Challenger.Domain.Contracts.Repositories;
using Challenger.Domain.DbModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Challenger.Infrastructure.Repositories
{
    public class IngridientRepository : IIngridientRepository
    {
        private readonly ChallengerFoodContext _context;

        public IngridientRepository(ChallengerFoodContext context)
        {
            _context = context;
        }

        public void Add(Ingridient record)
        {
            _context.Ingridients.Add(record);
        }

        public ValueTask<Ingridient> Get(long id)
        {
            return _context.Ingridients.FindAsync(id);
        }

        public Task<List<Ingridient>> GetAll()
        {
            return _context.Ingridients
                .Include(x => x.Product)
                //.Include(x => x.Dish)
                .ToListAsync();
        }

        public void Remove(Ingridient record)
        {
            _context.Ingridients.Remove(record);
        }

        public Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }

        public async Task Update(Ingridient record)
        {
            var dbRecord = await _context.Ingridients.FindAsync(record.Id);
            _context.Entry(dbRecord).CurrentValues.SetValues(record);
        }
    }
}
