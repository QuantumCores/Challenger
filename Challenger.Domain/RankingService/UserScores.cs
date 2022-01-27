namespace Challenger.Domain.RankingService
{
    public class UserScores
    {
        public string UserName { get; set; }

        public List<RankingScore> Scores { get; set; }
    }
}
