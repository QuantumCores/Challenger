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
        private static readonly Dictionary<string, ChallengeFormulaStore> _defaultFormulaCache = new Dictionary<string, ChallengeFormulaStore>();
        private static readonly Dictionary<long, ChallengeFormulaStore> _formulaCache = new Dictionary<long, ChallengeFormulaStore>();
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

        public async Task<ChallengeFormulaStore> GetFormulas(Challenge challenge)
        {
            if (_defaultFormulaCache.Count == 0)
            {
                await CacheFormulas();
            }

            if (!_formulaCache.ContainsKey(challenge.Id))
            {
                CacheFormulas(challenge);
            }

            return _formulaCache[challenge.Id];
        }

        private async Task CacheFormulas()
        {
            CacheDefaultFormulas(_settings);

            var customFormulas = await _challengeRepository.GetWithCustomFormulas();

            foreach (var challenge in customFormulas)
            {
                CacheFormulas(challenge);
            }
        }

        private void CacheFormulas(Challenge challenge)
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

        private void CacheDefaultFormulas(DefaultForumulaSetting[] settings)
        {
            foreach (var setting in settings)
            {
                var fit = setting.Type == FormulaTypeEnum.Fit ? CompileFormula<FitFormulaRecord>(setting.Formula) : null;
                var gym = setting.Type == FormulaTypeEnum.Gym ? CompileFormula<GymFormulaRecord>(setting.Formula) : null;
                var mes = setting.Type == FormulaTypeEnum.Measurement ? CompileFormula<MeasurementFormulaRecord>(setting.Formula) : null;

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
            if (string.IsNullOrWhiteSpace(formula))
            {
                return new FormulaValidationResult { IsValid = true };
            }

            var arg = new FitFormulaRecord();
            var argT = new FitFormulaRecord[] { arg };
            var result = ValidateFormula(formula, arg, argT);

            return new FormulaValidationResult { IsValid = result.IsValid, FitValidationMesage = result.Message };
        }

        public FormulaValidationResult ValidateGymFormula(string formula)
        {
            if (string.IsNullOrWhiteSpace(formula))
            {
                return new FormulaValidationResult { IsValid = true };
            }

            var arg = new GymFormulaRecord();
            var argT = new GymFormulaRecord[] { arg };
            var result = ValidateFormula(formula, arg, argT);

            return new FormulaValidationResult { IsValid = result.IsValid, GymValidationMesage = result.Message };
        }

        public FormulaValidationResult ValidateMeasurementFormula(string formula)
        {
            if (string.IsNullOrWhiteSpace(formula))
            {
                return new FormulaValidationResult { IsValid = true };
            }

            var arg = new MeasurementFormulaRecord();
            var argT = new MeasurementFormulaRecord[] { arg };
            var result = ValidateFormula(formula, arg, argT);

            return new FormulaValidationResult { IsValid = result.IsValid, MeasurementValidationMesage = result.Message };
        }

        private (bool IsValid, string Message) ValidateFormula<T>(string formula, T argument, T[] argumentT)
            where T : class, new()
        {
            try
            {
                var comp = CompileFormula<T>(formula);
                var result = comp(argument, argumentT);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return (false, ex.Message);
            }

            return (true, null);
        }

        private Func<T, T[], double> CompileFormula<T>(string formula)
        {
            if (string.IsNullOrWhiteSpace(formula))
            {
                return null;
            }

            var rpn = RPNParser.Parse(formula);
            var expr = ExpressionBuilder.Build<T>(rpn.Output);
            var par = new FitRecord { Distance = 12 };
            return expr.Compile();
        }
    }
}
