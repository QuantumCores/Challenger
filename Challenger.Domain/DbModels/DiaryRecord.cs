using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Challenger.Domain.DbModels
{
    public class DiaryRecord
    {
        public long Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public long UserId { get; set; }

        public User User { get; set; }

        public DateTime DiaryDate { get; set; }

        public List<MealRecord> MealRecords { get; set; }

        public double Energy { get; set; }

        public double Fats { get; set; }

        public double Proteins { get; set; }

        public double Carbohydrates { get; set; }
    }
}
