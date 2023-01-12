using QuantumCore.Repository.Entities;
using System;

namespace Challenger.Domain.Dtos
{
    public class UserChallengeDto : Entity
    {
        public Guid UserCorrelationId { get; set; }

        public long UserId { get; set; }

        public UserDto User { get; set; }

        public long ChallengeId { get; set; }

        public ChallengeDto Challenge { get; set; }
    }
}
