using Challenger.Domain.DbModels;
using Challenger.Domain.FormulaService;
using System.Threading.Tasks;

namespace Challenger.Domain.Contracts.Services
{
    public interface IFormulaService
    {
        Task<ChallengeFormulaStore> GetFormulas(Challenge challenge);

        FormulaValidationResult ValidateFitFormula(string formula);

        FormulaValidationResult ValidateGymFormula(string formula);

        FormulaValidationResult ValidateMeasurementFormula(string formula);
    }
}