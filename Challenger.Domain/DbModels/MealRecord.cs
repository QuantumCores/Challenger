using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Challenger.Domain.DbModels
{
    public class MealRecord
    {
        public long Id { get; set; }

        public bool IsNextDay { get; set; }

        [MaxLength(64)]
        public string MealName { get; set; }

        public int RecordTime { get; set; }

        [Required]
        [ForeignKey("DiaryRecord")]
        public long DiaryRecordId { get; set; }

        public DiaryRecord DiaryRecord { get; set; }

        public List<MealProduct> MealProducts { get; set; }

        public List<Dish> Dishes { get; set; }
    }
}
