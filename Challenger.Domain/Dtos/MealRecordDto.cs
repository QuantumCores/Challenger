namespace Challenger.Domain.Dtos
{
    public class MealRecordDto
    {
        public long Id { get; set; }

        public bool IsNextDay { get; set; }
        
        public string MealName { get; set; }

        public int RecordTime { get; set; }

        public long DiaryRecordId { get; set; }

        public List<MealProductDto> MealProducts { get; set; }

        public List<DishDto> Dishes { get; set; }
    }
}
