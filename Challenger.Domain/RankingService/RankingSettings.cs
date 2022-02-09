namespace Challenger.Domain.RankingService
{
    public class RankingSettings
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public double CaloriesPerPoint { get; set; }

        public int StepsPerPoint { get; set; }

        public Dictionary<string, double> CaloriesPerHourActivity { get; set; }
    }
}
