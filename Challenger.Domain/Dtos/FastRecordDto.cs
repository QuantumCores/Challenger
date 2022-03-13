using Challenger.Domain.Food;

namespace Challenger.Domain.Dtos
{
    public class FastRecordDto : INutrients
    {
        public long Id { get; set; }

        public long MealRecordId { get; set; }

        //public MealRecordDto MealRecord { get; set; }

        public string Comment { get; set; }

        public int Energy { get; set; }

        public int Fats { get; set; }

        public int Proteins { get; set; }

        public int Carbohydrates { get; set; }
    }
}
