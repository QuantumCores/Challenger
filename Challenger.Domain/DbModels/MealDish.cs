using Challenger.Domain.Food;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Challenger.Domain.DbModels
{
    public class MealDish : INutrients
    {
        public long Id { get; set; }

        [Required]
        [ForeignKey("MealRecord")]
        public long MealRecordId { get; set; }

        public MealRecord MealRecord { get; set; }

        [Required]
        [ForeignKey("Dish")]
        public long DishId { get; set; }

        public double Servings { get; set; }

        public Dish Dish { get; set; }

        public int Energy { get; set; }

        public int Fats { get; set; }

        public int Proteins { get; set; }

        public int Carbohydrates { get; set; }
    }
}
