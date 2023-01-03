using Challenger.Domain.Contracts.Repositories;
using Challenger.Domain.DbModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Challenger.Infrastructure.Repositories
{
    public class MealProductRepository : IMealProductRepository
    {
        private readonly ChallengerFoodContext _context;

        public MealProductRepository(ChallengerFoodContext context)
        {
            _context = context;
        }

        public void Add(MealProduct record)
        {
            _context.MealProducts.Add(record);
        }

        public ValueTask<MealProduct> Get(long id)
        {
            return _context.MealProducts.FindAsync(id);
        }

        public Task<List<MealProduct>> GetAll()
        {
            return _context.MealProducts
                //.Include(x => x.Product)
                .ToListAsync();
        }

        //public Task<List<MealProduct>> GetAllByDate(DateTime date)
        //{
        //    return _context.MealProducts
        //        .Include(x => x.Dishes)
        //        .Include(x => x.Products)                
        //        .ToListAsync();
        //}

        public void Remove(MealProduct record)
        {
            _context.MealProducts.Remove(record);
        }

        public Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }

        public async Task Update(MealProduct record)
        {
            var dbRecord = await _context.MealProducts.FindAsync(record.Id);
            _context.Entry(dbRecord).CurrentValues.SetValues(record);
        }
    }
}
