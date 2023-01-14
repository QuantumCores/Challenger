using Challenger.Domain.Contracts.Repositories;
using Challenger.Domain.Contracts.Services;
using Challenger.Domain.DbModels;
using Challenger.Domain.FormulaParser;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Challenger.Domain.FormulaService
{
    public class FormulaService : IFormulaService
    {
        private readonly Dictionary<string, ChallengeFormulaStore> _defaultFormulaCache;
        private readonly Dictionary<long, ChallengeFormulaStore> _formulaCache;
        private readonly DefaultForumulaSetting[] _settings;
        private readonly IChallengeRepository _challengeRepository;
        private readonly ILogger<FormulaService> _logger;

        public FormulaService(
            DefaultForumulaSetting[] settings,
            IChallengeRepository challengeRepository,
            ILogger<FormulaService> logger)
        {
            _settings = settings;
            _challengeRepository = challengeRepository;
            _logger = logger;
        }

        public async Task<ChallengeFormulaStore> GetFormulas(long challengeId)
        {
            if (_defaultFormulaCache == null)
            {
                await CacheFormulas();
            }

            return _formulaCache[challengeId];
        }

        private async Task CacheFormulas()
        {
            CacheDefaultFormulas(_settings);

            var customFormulas = await _challengeRepository.GetWithCustomFormulas();

            foreach (var challenge in customFormulas)
            {
                var fit = !challenge.IsUsingFitDefaultFormula ? CompileFormula<FitFormulaRecord>(challenge.FitFormula) : _defaultFormulaCache[challenge.FitFormula].FitFormula;
                var gym = !challenge.IsUsingGymDefaultFormula ? CompileFormula<GymFormulaRecord>(challenge.GymFormula) : _defaultFormulaCache[challenge.GymFormula].GymFormula;
                var mes = !challenge.IsUsingMeasurementDefaultFormula ? CompileFormula<MeasurementFormulaRecord>(challenge.MeasurementFormula) : _defaultFormulaCache[challenge.MeasurementFormula].MeasurementFormula;

                var tmp = new ChallengeFormulaStore
                {
                    FitFormula = fit,
                    GymFormula = gym,
                    MeasurementFormula = mes,
                };

                _formulaCache.Add(challenge.Id, tmp);
            }
        }

        private void CacheDefaultFormulas(DefaultForumulaSetting[] settings)
        {
            foreach (var setting in settings)
            {
                var fit = setting.Type == FormulaTypeEnum.Fit ? CompileFormula<FitFormulaRecord>(setting.Formula) : null;
                var gym = setting.Type == FormulaTypeEnum.Fit ? CompileFormula<GymFormulaRecord>(setting.Formula) : null;
                var mes = setting.Type == FormulaTypeEnum.Fit ? CompileFormula<MeasurementFormulaRecord>(setting.Formula) : null;

                var tmp = new ChallengeFormulaStore
                {
                    FitFormula = fit,
                    GymFormula = gym,
                    MeasurementFormula = mes,
                };

                _defaultFormulaCache.Add(setting.Name, tmp);
            }
        }

        public FormulaValidationResult ValidateFitFormula(string formula)
        {
            var arg = new FitFormulaRecord();
            var result = ValidateFormula(formula, arg);

            return new FormulaValidationResult { IsValid = result.IsValid, FitValidationMesage = result.Message };
        }

        public FormulaValidationResult ValidateGymFormula(string formula)
        {
            var arg = new GymFormulaRecord();
            var result = ValidateFormula(formula, arg);

            return new FormulaValidationResult { IsValid = result.IsValid, GymValidationMesage = result.Message };
        }

        public FormulaValidationResult ValidateMeasurementFormula(string formula)
        {
            var arg = new MeasurementFormulaRecord();
            var result = ValidateFormula(formula, arg);

            return new FormulaValidationResult { IsValid = result.IsValid, MeasurementValidationMesage = result.Message };
        }

        private (bool IsValid, string Message) ValidateFormula<T>(string formula, T argument)
            where T : class, new()
        {
            try
            {
                var comp = CompileFormula<T>(formula);
                var result = comp(argument);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return (false, ex.Message);
            }

            return (true, null);
        }

        private Func<T, double> CompileFormula<T>(string formula)
        {
            var rpn = RPNParser.Parse(formula);
            var expr = ExpressionBuilder.Build<T>(rpn.Output);
            var par = new FitRecord { Distance = 12 };
            return expr.Compile();
        }
    }
}
