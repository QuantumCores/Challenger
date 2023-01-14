using Challenger.Domain.DbModels;
using Challenger.Domain.Dtos;
using System.Threading.Tasks;

namespace Challenger.Domain.Contracts.Services
{
    public interface IChallengeService
    {
        Task<Challenge> CreateChallenge(ChallengeDto challenge);
    }
}