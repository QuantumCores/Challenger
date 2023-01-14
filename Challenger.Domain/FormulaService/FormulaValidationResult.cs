namespace Challenger.Domain.FormulaService
{
    public class FormulaValidationResult
    {
        public bool IsValid { get; set; }

        public string FitValidationMesage { get; set; }

        public string GymValidationMesage { get; set; }

        public string MeasurementValidationMesage { get; set; }
    }
}
