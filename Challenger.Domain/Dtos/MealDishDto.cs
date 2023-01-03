using Challenger.Domain.Food;

namespace Challenger.Domain.Dtos
{
    public class MealDishDto : INutrients
    {
        public long Id { get; set; }

        public long MealRecordId { get; set; }

        public long DishId { get; set; }

        public string DishName  { get; set; }

        public double Servings { get; set; }

        public double Energy { get; set; }

        public double Fats { get; set; }

        public double Proteins { get; set; }

        public double Carbohydrates { get; set; }
    }
}
