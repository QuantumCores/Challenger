﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Challenger.Domain.DbModels
{
    public class FitRecord
    {
        public long Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public long UserId { get; set; }
        
        public User User { get; set; }

        public DateTime RecordDate { get; set; }

        public long ActivityTypeId { get; set; }

        public ActivityType ActivityType { get; set; }

        public int? Duration { get; set; }

        public string DurationUnit { get; set; }

        public double? Distance { get; set; }

        public double? Repetitions { get; set; }

        public double BurntCalories { get; set; }
    }
}
