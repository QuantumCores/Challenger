using System;

namespace Challenger.Domain.Dtos
{
    public class FitRecordDto
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public DateTime RecordDate { get; set; }

        public string Excersize { get; set; }

        public int? Duration { get; set; }

        public string? DurationUnit { get; set; }

        public double? Distance { get; set; }

        public double? Repetitions { get; set; }

        public double BurntCalories { get; set; }
    }
}
