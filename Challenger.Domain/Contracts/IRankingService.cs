using Challenger.Domain.RankingService;

namespace Challenger.Domain.Contracts
{
    public interface IRankingService
    {
        Task<List<UserScores>> GetScores();
    }
}
