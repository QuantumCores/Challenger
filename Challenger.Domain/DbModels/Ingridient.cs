using Challenger.Domain.Food;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Challenger.Domain.DbModels
{
    public class Ingridient : INutrients
    {
        public long Id { get; set; }

        [Required]
        [ForeignKey("Dish")]
        public long DishId { get; set; }

        public Dish Dish { get; set; }

        [Required]
        [ForeignKey("Product")]
        public long ProductId { get; set; }

        public Product Product { get; set; }

        public int Size { get; set; }

        public double Energy { get; set; }

        public double Fats { get; set; }

        public double Proteins { get; set; }

        public double Carbohydrates { get; set; }
    }
}
