using AutoMapper;
using Challenger.Domain.Contracts.Repositories;
using Challenger.Domain.Contracts.Services;
using Challenger.Domain.DbModels;
using Challenger.Domain.FormulaService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenger.Domain.RankingService
{
    public class RankingService : IRankingService
    {
        private readonly IChallengeRepository _challengeRepository;
        private readonly IGymRecordRepository _gymRecordRepository;
        private readonly IFitRecordRepository _fitRecordRepository;
        private readonly IMeasurementRepository _measurementRepository;
        private readonly IFormulaService _formulaService;
        private readonly IMapper _mapper;

        public RankingService(
            IChallengeRepository challengeRepository,
            IGymRecordRepository gymRecordRepository,
            IFitRecordRepository fitRecordRepository,
            IMeasurementRepository measurementRepository,
            IFormulaService formulaService,
            IMapper mapper)
        {
            _challengeRepository = challengeRepository;
            _gymRecordRepository = gymRecordRepository;
            _fitRecordRepository = fitRecordRepository;
            _measurementRepository = measurementRepository;
            _formulaService = formulaService;
            _mapper = mapper;
        }

        public async Task<List<UserScores>> GetScores(long challengeId)
        {
            var challenge = await _challengeRepository.Get(challengeId);
            var formulas = await _formulaService.GetFormulas(challenge);

            var usersIds = challenge.Participants.Select(x => x.UserCorrelationId).ToArray();

            var fitRecords = await _fitRecordRepository.GetAllByTimeRange(challenge.StartDate, challenge.EndDate, usersIds);
            var gymRecords = await _gymRecordRepository.GetAllByTimeRange(challenge.StartDate, challenge.EndDate, usersIds);
            var mesRecords = await _measurementRepository.GetAllByTimeRange(challenge.StartDate, challenge.EndDate, usersIds);

            var userFitRecords = fitRecords.ToLookup(x => x.User.CorrelationId, x => _mapper.Map<FitFormulaRecord>(x));
            var userGymRecords = gymRecords.ToLookup(x => x.User.CorrelationId, x => _mapper.Map<GymFormulaRecord>(x));
            var userMesRecords = mesRecords.ToLookup(x => x.User.CorrelationId, x => _mapper.Map<MeasurementFormulaRecord>(x));

            var usersDictionary = usersIds.ToHashSet().ToDictionary(x => x, x => new Dictionary<DateTime, RankingScore>());

            var result = new List<UserScores>();
            foreach (var userKey in usersDictionary.Keys)
            {
                var userScore = new UserScores { CorrelationId = userKey };

                if (userFitRecords.Contains(userKey) && formulas.FitFormula != null)
                {
                    var fitArray = userFitRecords[userKey].ToArray();
                    var byDate = fitArray.GroupBy(x => x.RecordDate.Date);

                    foreach (var dateGroup in byDate)
                    {
                        var dateSum = dateGroup.Sum(x => formulas.FitFormula(x, fitArray));

                        if (!usersDictionary[userKey].ContainsKey(dateGroup.Key))
                        {
                            usersDictionary[userKey].Add(dateGroup.Key, new RankingScore() { Date = dateGroup.Key });
                        }

                        usersDictionary[userKey][dateGroup.Key].Score += dateSum;
                    }
                }

                if (userGymRecords.Contains(userKey) && formulas.GymFormula != null)
                {
                    var gymArray = userGymRecords[userKey].ToArray();
                    var byDate = gymArray.GroupBy(x => x.RecordDate.Date);

                    foreach (var dateGroup in byDate)
                    {
                        var dateSum = dateGroup.Sum(x => formulas.GymFormula(x, gymArray));

                        if (!usersDictionary[userKey].ContainsKey(dateGroup.Key))
                        {
                            usersDictionary[userKey].Add(dateGroup.Key, new RankingScore() { Date = dateGroup.Key });
                        }

                        usersDictionary[userKey][dateGroup.Key].Score += dateSum;
                    }
                }

                if (userMesRecords.Contains(userKey) && formulas.MeasurementFormula != null)
                {
                    var mesArray = userMesRecords[userKey].ToArray();
                    var byDate = mesArray.GroupBy(x => x.MeasurementDate.Date);

                    foreach (var dateGroup in byDate)
                    {
                        var dateSum = dateGroup.Sum(x => formulas.MeasurementFormula(x, mesArray));

                        if (!usersDictionary[userKey].ContainsKey(dateGroup.Key))
                        {
                            usersDictionary[userKey].Add(dateGroup.Key, new RankingScore() { Date = dateGroup.Key });
                        }

                        usersDictionary[userKey][dateGroup.Key].Score += dateSum;
                    }
                }

                var ordered = usersDictionary[userKey].OrderBy(x => x.Key);
                var fullScore = 0.0;
                foreach (var scoreByDate in ordered)
                {
                    scoreByDate.Value.FullScore = fullScore += scoreByDate.Value.Score;
                }

                userScore.Scores = ordered.Select(x => x.Value).ToList();
                result.Add(userScore);
            }

            return result;
        }

        public async Task<List<double?>> GetFatPercentage()
        {
            var measurements = await _measurementRepository.GetAll();

            if (!measurements.Any() || !measurements[0].User.Height.HasValue)
            {
                return new List<double?> { 0.0 };
            }

            return measurements.Select(x => x.Fat.HasValue ? x.Fat : CalculateFat(x))
                               .ToList();
        }

        private double? CalculateFat(Measurement measurement)
        {
            if (true)
            {
                if (measurement.Waist.HasValue && measurement.Neck.HasValue)
                {
                    return 495.0 / (1.0324 - 0.19077 * Math.Log10(measurement.Waist.Value - measurement.Neck.Value) + 0.15456 * Math.Log10(measurement.User.Height.Value)) - 450;
                }
            }
            else
            {
                return 495.0 / (1.29579 - 0.35004 * Math.Log10(measurement.Waist.Value + measurement.Hips.Value - measurement.Neck.Value) + 0.22100 * Math.Log10(measurement.User.Height.Value)) - 450;
            }

            return null;
        }
    }
}
