using Challenger.Domain.Food;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Challenger.Domain.DbModels
{
    public class MealProduct : INutrients
    {
        public long Id { get; set; }

        [Required]
        [ForeignKey("Product")]
        public long ProductId { get; set; }

        public Product Product { get; set; }

        [Required]
        [ForeignKey("MealRecord")]
        public long MealRecordId { get; set; }

        public MealRecord MealRecord { get; set; }

        public int Size { get; set; }

        public double Energy { get; set; }

        public double Fats { get; set; }

        public double Proteins { get; set; }

        public double Carbohydrates { get; set; }
    }
}
