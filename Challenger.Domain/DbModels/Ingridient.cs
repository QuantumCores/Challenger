using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Challenger.Domain.DbModels
{
    public class Ingridient
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
    }
}
