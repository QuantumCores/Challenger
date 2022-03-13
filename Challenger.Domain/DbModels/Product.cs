using Challenger.Domain.Food;
using System.ComponentModel.DataAnnotations;

namespace Challenger.Domain.DbModels
{
    public class Product : INutrients
    {
        public long Id { get; set; }

        [MaxLength(64)]
        public string Name { get; set; }

        [MaxLength(32)]
        public string Brand { get; set; }

        public long? Barcode { get; set; }

        public int Size { get; set; }

        [MaxLength(1)]
        public string SizeUnit { get; set; }

        public int Energy { get; set; }

        public int Fats { get; set; }

        public int Proteins { get; set; }

        public int Carbohydrates { get; set; }
    }
}
