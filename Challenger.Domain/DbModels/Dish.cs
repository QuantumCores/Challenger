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

        public int Energy { get; set; }

        public int Fats { get; set; }

        public int Proteins { get; set; }

        public int Carbohydrates { get; set; }
    }
}
