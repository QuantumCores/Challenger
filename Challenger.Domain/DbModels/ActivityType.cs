﻿namespace Challenger.Domain.DbModels
{
    public class ActivityType
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double MET { get; set; }
    }
}
