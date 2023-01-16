using System;

namespace Challenger.Domain.RankingService
{
    public class RankingScore
    {
        public double FitScore { get; set; }

        public double GymScore { get; set; }

        public double MeasurementScore { get; set; }

        public double FullFitScore { get; set; }

        public double FullGymScore { get; set; }

        public double FullMeasurementScore { get; set; }

        public DateTime Date { get; set; }
    }
}
