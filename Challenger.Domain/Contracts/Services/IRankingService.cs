using Challenger.Domain.RankingService;
using System.Threading.Tasks;

namespace Challenger.Domain.Contracts.Services
{
    public interface IRankingService
    {
        Task<ChallengeScores> GetScores(long challengeId);
    }
}
