using System;

namespace Challenger.Domain.FormulaService
{
    public class ChallengeFormulaStore
    {
        public Func<FitFormulaRecord, FitFormulaRecord[], double> FitFormula { get; set; }

        public Func<GymFormulaRecord, GymFormulaRecord[], double> GymFormula { get; set; }

        public Func<MeasurementFormulaRecord, MeasurementFormulaRecord[], double> MeasurementFormula { get; set; }
    }
}
