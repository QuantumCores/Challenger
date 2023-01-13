using QuantumCore.Repository.Entities;
using System;
using System.Collections.Generic;

namespace Challenger.Domain.DbModels
{
    public class Challenge : Entity
    {
        public Guid CreatorId { get; set; }

        public long UserId { get; set; }

        public User User { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Formula { get; set; }

        public List<UserChallenge> Participants { get; set; }
    }
}
