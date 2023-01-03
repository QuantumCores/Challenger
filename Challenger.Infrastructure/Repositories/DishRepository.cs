using Challenger.Domain.Contracts.Repositories;
using Challenger.Domain.DbModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenger.Infrastructure.Repositories
{
    public class DishRepository : IDishRepository
    {
        private const int MaxResultCount = 10;
        private readonly ChallengerFoodContext _context;

        public DishRepository(ChallengerFoodContext context)
        {
            _context = context;
        }

        public void Add(Dish record)
        {
            _context.Dishes.Add(record);
        }

        public ValueTask<Dish> Get(long id)
        {
            return _context.Dishes.FindAsync(id);
        }

        public Task<List<Dish>> GetAll()
        {
            return _context.Dishes.Include(x => x.User).ToListAsync();
        }

        public Task<List<Dish>> GetAllForUser(long userId)
        {
            return _context.Dishes.Where(x => x.UserId == userId).ToListAsync();
        }

        public Task<List<Dish>> Find(string search)
        {
            return _context.Dishes.Where(x => x.Name.Contains(search))
                .Take(MaxResultCount)
                .ToListAsync();
        }

        public void Remove(Dish record)
        {
            _context.Dishes.Remove(record);
        }

        public Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }

        public async Task Update(Dish record)
        {
            var dbRecord = await _context.Dishes.FindAsync(record.Id);
            _context.Entry(dbRecord).CurrentValues.SetValues(record);
        }
    }
}
