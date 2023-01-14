using System;

namespace Challenger.Domain.FormulaService
{
    public class ChallengeFormulaStore
    {
        public Func<FitFormulaRecord, double> FitFormula { get; set; }

        public Func<GymFormulaRecord, double> GymFormula { get; set; }

        public Func<MeasurementFormulaRecord, double> MeasurementFormula { get; set; }
    }
}
