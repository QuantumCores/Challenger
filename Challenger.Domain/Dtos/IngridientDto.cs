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

        public int Energy { get; set; }

        public int Fats { get; set; }

        public int Proteins { get; set; }

        public int Carbohydrates { get; set; }
    }
}
