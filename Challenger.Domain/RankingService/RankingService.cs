using Challenger.Domain.Contracts;
using Challenger.Domain.DbModels;

namespace Challenger.Domain.RankingService
{
    public class RankingService : IRankingService
    {
        private readonly RankingSettings _settings;
        private readonly IGymRecordRepository _gymRecordRepository;
        private readonly IFitRecordRepository _fitRecordRepository;
        private readonly IMeasurementRepository _measurementRepository;

        public RankingService(
            RankingSettings settings,
            IGymRecordRepository gymRecordRepository,
            IFitRecordRepository fitRecordRepository,
            IMeasurementRepository measurementRepository)
        {
            _settings = settings;
            _gymRecordRepository = gymRecordRepository;
            _fitRecordRepository = fitRecordRepository;
            _measurementRepository = measurementRepository;
        }

        public async Task<List<UserScores>> GetScores()
        {
            var fitRecords = await _fitRecordRepository.GetAllByTimeRange(_settings.StartDate, _settings.EndDate);
            var gymrecords = await _gymRecordRepository.GetAllByTimeRange(_settings.StartDate, _settings.EndDate);

            var userFitRecords = fitRecords.ToLookup(x => x.User.UserName);
            var userGymRecords = gymrecords.ToLookup(x => x.User.UserName);

            var users = userFitRecords.Select(x => x.Key).ToList();
            users.AddRange(userGymRecords.Select(x => x.Key));

            var usersDictionary = users.ToHashSet().ToDictionary(x => x, x => new Dictionary<DateTime, RankingScore>());

            var result = new List<UserScores>();
            foreach (var userKey in usersDictionary.Keys)
            {
                var userScore = new UserScores { UserName = userKey };

                if (userFitRecords.Contains(userKey))
                {
                    var byDate = userFitRecords[userKey].GroupBy(x => x.RecordDate.Date);

                    foreach (var dateGroup in byDate)
                    {
                        var dateSum = dateGroup.Sum(x => CalculateScore(x));

                        if (!usersDictionary[userKey].ContainsKey(dateGroup.Key))
                        {
                            usersDictionary[userKey].Add(dateGroup.Key, new RankingScore() { Date = dateGroup.Key });
                        }

                        usersDictionary[userKey][dateGroup.Key].Score += dateSum;
                    }
                }

                if (userGymRecords.Contains(userKey))
                {
                    var byDate = userGymRecords[userKey].GroupBy(x => x.RecordDate.Date);
                    var fullSum = 0.0;
                    foreach (var dateGroup in byDate)
                    {
                        var dateSum = dateGroup.Sum(x => CalculateScore(x));

                        if (!usersDictionary[userKey].ContainsKey(dateGroup.Key))
                        {
                            usersDictionary[userKey].Add(dateGroup.Key, new RankingScore() { Date = dateGroup.Key });
                        }

                        fullSum += dateSum;
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

            return measurements.Select(x => x.Fat.HasValue ? x.Fat : CalculateFat(x))
                               .ToList();
        }

        private double? CalculateFat(Measurement measurement)
        {
            if (true)
            {
                if (measurement.Waist.HasValue && measurement.Neck.HasValue)
                {
                    return 495.0 / (1.0324 - 0.19077 * Math.Log10(measurement.Waist.Value - measurement.Neck.Value) + 0.15456 * Math.Log10(measurement.User.Height)) - 450;
                }
            }
            else
            {
                return 495.0 / (1.29579 - 0.35004 * Math.Log10(measurement.Waist.Value + measurement.Hips.Value - measurement.Neck.Value) + 0.22100 * Math.Log10(measurement.User.Height)) - 450;
            }

            return null;
        }

        private double CalculateScore(FitRecord record)
        {
            if (record.Excersize == nameof(FitExcersizesEnum.Walking))
            {
                if (record.Repetitions.HasValue && record.Repetitions >= 10000)
                {
                    return record.Repetitions.Value / _settings.StepsPerPoint;
                }
            }
            else
            {
                if (record.BurntCalories != 0)
                {
                    return record.BurntCalories / _settings.CaloriesPerPoint;
                }
                else
                {
                    if (record.Duration.HasValue && record.Duration.Value != 0 && record.DurationUnit != null)
                    {
                        var time = TimeHelper.CalculateTimeInMinutes(record.Duration.Value, record.DurationUnit);
                        return (180.0 * time / 60.0) / _settings.CaloriesPerPoint;
                    }
                }
            }

            return 0;
        }

        private double CalculateScore(GymRecord record)
        {
            return 1;
        }
    }
}
