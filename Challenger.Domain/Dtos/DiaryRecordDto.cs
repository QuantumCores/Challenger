using System;
using System.Collections.Generic;

namespace Challenger.Domain.Dtos
{
    public class DiaryRecordDto
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public DateTime DiaryDate { get; set; }

        public List<MealRecordDto> MealRecords { get; set; }

        public double Energy { get; set; }

        public double Fats { get; set; }

        public double Proteins { get; set; }

        public double Carbohydrates { get; set; }
    }
}
