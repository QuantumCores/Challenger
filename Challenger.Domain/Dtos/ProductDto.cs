using Challenger.Domain.Food;

namespace Challenger.Domain.Dtos
{
    public  class ProductDto : INutrients
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public string Brand { get; set; }

        public long? Barcode { get; set; }

        public int Size { get; set; }

        public string SizeUnit { get; set; }

        public double Energy { get; set; }

        public double Fats { get; set; }

        public double Proteins { get; set; }

        public double Carbohydrates { get; set; }
    }
}
