using Challenger.Domain.DbModels;
using Challenger.Domain.Dtos;
using System;
using System.Threading.Tasks;

namespace Challenger.Domain.Contracts.Services
{
    public interface IChallengeService
    {
        Task<ChallengeDisplayDto[]> GetForUser(Guid userId);

        Task<Challenge> CreateChallenge(ChallengeDto challenge);

        Task<ChallengeDisplayDto[]> GetByName(Guid guid, string name);

        Task<bool> JoinChallenge(Guid userId, int challengeId);
    }
}