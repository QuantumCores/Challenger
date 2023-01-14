using Challenger.Domain.RankingService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Challenger.Domain.Contracts.Services
{
    public interface IRankingService
    {
        Task<List<UserScores>> GetScores();
    }
}
