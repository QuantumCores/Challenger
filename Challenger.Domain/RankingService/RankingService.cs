using Challenger.Domain.Contracts;
using Challenger.Domain.DbModels;

namespace Challenger.Domain.RankingService
{
    public class RankingService : IRankingService
    {
        private readonly IGymRecordRepository _gymRecordRepository;
        private readonly IFitRecordRepository _fitRecordRepository;

        public RankingService(
            IGymRecordRepository gymRecordRepository,
            IFitRecordRepository fitRecordRepository)
        {
            _gymRecordRepository = gymRecordRepository;
            _fitRecordRepository = fitRecordRepository;
        }

        public async Task<List<UserScores>> GetScores()
        {
            var fitRecords = await _fitRecordRepository.GetAll();
            var gymrecords = await _gymRecordRepository.GetAll();

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

        private double CalculateScore(FitRecord record)
        {
            return 1;
        }

        private double CalculateScore(GymRecord record)
        {
            return 1;
        }
    }
}
