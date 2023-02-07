using Challenger.Domain.Contracts.Identity;
using Challenger.Domain.DbModels;
using QuantumCore.Repository.Entities;
using System;
using System.Collections.Generic;

namespace Challenger.Domain.Dtos
{
    public class ChallengeDto : Entity
    {
        public Guid CreatorId { get; set; }

        public User User { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsUsingFitDefaultFormula { get; set; }

        public string FitFormula { get; set; }

        public bool AggregateFitFormula { get; set; }

        public bool IsUsingGymDefaultFormula { get; set; }

        public string GymFormula { get; set; }

        public bool AggregateGymFormula { get; set; }

        public bool IsUsingMeasurementDefaultFormula { get; set; }

        public string MeasurementFormula { get; set; }

        public bool AggregateMeasurementFormula { get; set; }

        public List<ApplicationUser> Participants { get; set; }
    }
}
