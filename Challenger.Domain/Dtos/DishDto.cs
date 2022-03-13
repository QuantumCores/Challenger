namespace Challenger.Domain.Dtos
{
    public class DishDto
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public List<IngridientDto> Ingridients { get; set; }

        public string Name { get; set; }

        public bool IsPublic { get; set; }

        public int PreparationTime { get; set; }

        public int Servings { get; set; }

        public int Energy { get; set; }

        public int Fats { get; set; }

        public int Proteins { get; set; }

        public int Carbohydrates { get; set; }
    }
}
