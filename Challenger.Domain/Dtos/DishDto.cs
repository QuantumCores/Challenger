namespace Challenger.Domain.Dtos
{
    public class DishDto
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public string Name { get; set; }

        public bool IsPublic { get; set; }

        public int PreparationTime { get; set; }

        public int Servings { get; set; }
    }
}
