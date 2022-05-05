using Challenger.Domain.Food;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Challenger.Domain.DbModels
{
    public class Dish : INutrients
    {
        public long Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public long UserId { get; set; }

        public User User { get; set; }

        [MaxLength(64)]
        public string Name { get; set; }

        public bool IsPublic { get; set; }

        public int PreparationTime { get; set; }

        public int Servings { get; set; }

        public List<Ingridient> Ingridients { get; set; }

        public double Energy { get; set; }

        public double Fats { get; set; }

        public double Proteins { get; set; }

        public double Carbohydrates { get; set; }
    }
}
