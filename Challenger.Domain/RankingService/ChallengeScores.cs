using System;
using System.Collections.Generic;

namespace Challenger.Domain.RankingService
{
    public class ChallengeScores
    {
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<UserScores> UsersScores { get; set; }
    }
}
