using Challenger.Domain.Food;

namespace Challenger.Domain.Dtos
{
    public  class ProductDto : Nutrients
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public string Brand { get; set; }

        public long? Barcode { get; set; }

        public int Size { get; set; }

        public string SizeUnit { get; set; }

        public int Energy { get; set; }

        public int Fats { get; set; }

        public int Proteins { get; set; }

        public int Carbohydrates { get; set; }
    }
}
