using System;

namespace Challenger.Domain.FormulaService
{
    public class FitFormulaRecord
    {
        public DateTime RecordDate { get; set; }

        public string Excersize { get; set; }

        public double Duration { get; set; }

        public double Distance { get; set; }

        public double Repetitions { get; set; }

        public double Calories { get; set; }
    }
}
