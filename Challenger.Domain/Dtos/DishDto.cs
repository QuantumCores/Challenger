using Challenger.Domain.Food;
using System.Collections.Generic;

namespace Challenger.Domain.Dtos
{
    public class DishDto : INutrients
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public List<IngridientDto> Ingridients { get; set; }

        public string Name { get; set; }

        public bool IsPublic { get; set; }

        public int PreparationTime { get; set; }

        public int Servings { get; set; }

        public double Energy { get; set; }

        public double Fats { get; set; }

        public double Proteins { get; set; }

        public double Carbohydrates { get; set; }
    }
}
