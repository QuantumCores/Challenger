using System;

namespace Challenger.Domain.FormulaService
{
    public class FitFormulaRecord
    {
        public DateTime RecordDate { get; set; }

        public string Excersize { get; set; }

        public int Duration { get; set; }

        public string DurationUnit { get; set; }

        public double Distance { get; set; }

        public double Repetitions { get; set; }

        public double BurntCalories { get; set; }
    }
}
