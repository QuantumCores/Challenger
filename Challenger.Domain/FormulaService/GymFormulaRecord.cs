using System;

namespace Challenger.Domain.FormulaService
{
    public class GymFormulaRecord
    {
        public DateTime RecordDate { get; set; }

        public string Excersize { get; set; }

        public double Weight { get; set; }

        public double Repetitions { get; set; }

        public string Muscle { get; set; }
    }
}
