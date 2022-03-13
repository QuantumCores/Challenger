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

        public int Energy { get; set; }

        public int Fats { get; set; }

        public int Proteins { get; set; }

        public int Carbohydrates { get; set; }
    }
}
