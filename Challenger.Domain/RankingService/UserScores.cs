using Challenger.Domain.Contracts.Identity;
using System.Collections.Generic;

namespace Challenger.Domain.RankingService
{
    public class UserScores
    {
        public ApplicationUser User { get; set; }

        public double TotalScore { get; set; }

        public double TotalFitScore { get; set; }

        public double TotalGymScore { get; set; }

        public double TotalMeasurementScore { get; set; }

        public List<RankingScore> Scores { get; set; }
    }
}
