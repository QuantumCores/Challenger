using Challenger.Domain.Food;

namespace Challenger.Domain.Dtos
{
    public class IngridientDto : INutrients
    {
        public long Id { get; set; }

        public long DishId { get; set; }

        public long ProductId { get; set; }

        public string ProductName { get; set; }

        public int Size { get; set; }

        public double Energy { get; set; }

        public double Fats { get; set; }

        public double Proteins { get; set; }

        public double Carbohydrates { get; set; }
    }
}
