using Challenger.Domain.Food;

namespace Challenger.Domain.Dtos
{
    public class MealProductDto : INutrients
    {
        public long Id { get; set; }

        public long ProductId { get; set; }

        public string ProductName { get; set; }

        public long MealRecordId { get; set; }

        public int Size { get; set; }

        public double Energy { get; set; }

        public double Fats { get; set; }

        public double Proteins { get; set; }

        public double Carbohydrates { get; set; }
    }
}
