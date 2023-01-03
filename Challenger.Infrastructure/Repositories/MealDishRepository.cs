using Challenger.Domain.Contracts.Repositories;
using Challenger.Domain.DbModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Challenger.Infrastructure.Repositories
{
    public class MealDishRepository : IMealDishRepository
    {
        private readonly ChallengerFoodContext _context;

        public MealDishRepository(ChallengerFoodContext context)
        {
            _context = context;
        }

        public void Add(MealDish record)
        {
            _context.MealDishes.Add(record);
        }

        public ValueTask<MealDish> Get(long id)
        {
            return _context.MealDishes.FindAsync(id);
        }

        public Task<List<MealDish>> GetAll()
        {
            return _context.MealDishes
                //.Include(x => x.Dish)
                .ToListAsync();
        }

        //public Task<List<MealDish>> GetAllByDate(DateTime date)
        //{
        //    return _context.MealDishes
        //        .Include(x => x.Dishes)
        //        .Include(x => x.Products)                
        //        .ToListAsync();
        //}

        public void Remove(MealDish record)
        {
            _context.MealDishes.Remove(record);
        }

        public Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }

        public async Task Update(MealDish record)
        {
            var dbRecord = await _context.MealDishes.FindAsync(record.Id);
            _context.Entry(dbRecord).CurrentValues.SetValues(record);
        }
    }
}
