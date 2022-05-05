using Challenger.Domain.Food;

namespace Challenger.Domain.Dtos
{
    public class FastRecordDto : INutrients
    {
        public long Id { get; set; }

        public long MealRecordId { get; set; }

        //public MealRecordDto MealRecord { get; set; }

        public string Comment { get; set; }

        public double Energy { get; set; }

        public double Fats { get; set; }

        public double Proteins { get; set; }

        public double Carbohydrates { get; set; }
    }
}
