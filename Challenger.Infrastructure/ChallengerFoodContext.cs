using Challenger.Domain.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Challenger.Infrastructure
{
    public class ChallengerFoodContext : DbContext
    {
        public ChallengerFoodContext(DbContextOptions<ChallengerFoodContext> options)
            : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Dish> Dishes { get; set; }

        public DbSet<Ingridient>  Ingridients { get; set; }

        public DbSet<MealRecord> MealRecords { get; set; }

        public DbSet<MealProduct> MealProducts { get; set; }

        public DbSet<MealDish> MealDishes { get; set; }

        public DbSet<FastRecord>  FastRecords { get; set; }

        public DbSet<DiaryRecord> DiaryRecords { get; set; }
    }
}
