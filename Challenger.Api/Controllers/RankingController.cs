using Challenger.Domain.Contracts;
using Challenger.Domain.RankingService;
using Microsoft.AspNetCore.Mvc;

namespace Challenger.Api.Controllers
{
    [ApiController]    
    [Route("[controller]")]
    public class RankingController : Controller
    {
        private readonly IRankingService _rankingService;

        public RankingController(IRankingService rankingService)
        {
            _rankingService = rankingService;
        }

        [HttpGet]
        public Task<List<UserScores>> Get()
        {
            return _rankingService.GetScores();
        }
    }
}
