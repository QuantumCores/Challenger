using Challenger.Domain.Contracts.Services;
using Challenger.Domain.RankingService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public Task<ChallengeScores> Get(long challengeId)
        {
            //var challenge = new Challenge
            //{
            //    Id = challengeId,
            //    StartDate = new System.DateTime(2022, 02, 01),
            //    EndDate = new System.DateTime(2023, 02, 01),
            //    FitFormula = "Callories burnt",
            //    IsUsingFitDefaultFormula = true,
            //    IsUsingGymDefaultFormula = true,
            //    GymFormula = "Heavy lifter",
            //    IsUsingMeasurementDefaultFormula = true,
            //    MeasurementFormula = "Weight loss",
            //    Participants = new List<UserChallenge> {
            //        new UserChallenge { Id = challengeId, ChallengeId = challengeId, UserCorrelationId = System.Guid.Parse("FCFEA281-4C90-414D-B76E-ED5AF8980877") },
            //        new UserChallenge { Id = challengeId, ChallengeId = challengeId, UserCorrelationId = System.Guid.Parse("2FA51EE3-F250-4565-95FB-5F46AF294301") },
            //    }
            //};
            return _rankingService.GetScores(challengeId);
        }
    }
}
