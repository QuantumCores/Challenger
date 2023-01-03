using Challenger.Domain.Contracts.Repositories;
using Challenger.Domain.DbModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenger.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private const int MaxResultCount = 10;
        private readonly ChallengerFoodContext _context;

        public ProductRepository(ChallengerFoodContext context)
        {
            _context = context;
        }

        public void Add(Product record)
        {
            _context.Products.Add(record);
        }

        public ValueTask<Product> Get(long id)
        {
            return _context.Products.FindAsync(id);
        }

        public Task<List<Product>> Find(string search)
        {
            return _context.Products.Where(x => x.Name.Contains(search) || x.Brand.Contains(search))
                .Take(MaxResultCount)
                .ToListAsync();
        }

        public Task<List<Product>> GetAll()
        {
            return _context.Products.ToListAsync();
        }

        public void Remove(Product record)
        {
            _context.Products.Remove(record);
        }

        public Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }

        public async Task Update(Product record)
        {
            var dbRecord = await _context.Products.FindAsync(record.Id);
            _context.Entry(dbRecord).CurrentValues.SetValues(record);
        }
    }
}
