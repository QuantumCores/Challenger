namespace Challenger.Domain.Dtos
{
    public class DiaryRecordDto
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public DateTime DiaryDate { get; set; }

        public List<MealRecordDto> MealRecords { get; set; }

        public int Energy { get; set; }

        public int Fats { get; set; }

        public int Proteins { get; set; }

        public int Carbohydrates { get; set; }
    }
}
