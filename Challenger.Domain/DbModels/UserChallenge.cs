using QuantumCore.Repository.Entities;
using System;

namespace Challenger.Domain.DbModels
{
    public class UserChallenge : Entity
    {        
        public Guid UserCorrelationId { get; set; }

        public long UserId { get; set; }

        public User User { get; set; }

        public long ChallengeId { get; set; }

        public Challenge Challenge { get; set; }
    }
}
