using Challenger.Domain.Food;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Challenger.Domain.DbModels
{
    public class FastRecord : INutrients
    {
        public long Id { get; set; }

        [Required]
        [ForeignKey("MealRecord")]
        public long MealRecordId { get; set; }

        public MealRecord MealRecord { get; set; }

        public string Comment { get; set; }

        public int Energy { get; set; }

        public int Fats { get; set; }

        public int Proteins { get; set; }

        public int Carbohydrates { get; set; }
    }
}
