using System;
using System.Collections.Generic;

namespace Challenger.Domain.RankingService
{
    public class UserScores
    {
        public Guid CorrelationId { get; set; }

        public double TotalScore { get; set; }

        public List<RankingScore> Scores { get; set; }
    }
}
