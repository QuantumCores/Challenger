using Challenger.Domain.DbModels;
using Challenger.Domain.Dtos;
using System.Threading.Tasks;

namespace Challenger.Domain.Contracts
{
    public interface IChallengeService
    {
        Task<Challenge> CreateChallenge(ChallengeDto challenge);
    }
}