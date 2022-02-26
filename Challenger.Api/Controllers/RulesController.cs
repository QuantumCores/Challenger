using AutoMapper;
using Challenger.Domain.Dtos;
using Challenger.Domain.RankingService;
using Microsoft.AspNetCore.Mvc;

namespace Challenger.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RulesController : Controller
    {
        private readonly RankingSettings _rankingSettings;
        private readonly IMapper _mapper;

        public RulesController(
            RankingSettings rankingSettings,
            IMapper mapper)
        {
            _rankingSettings = rankingSettings;
            _mapper = mapper;
        }

        [HttpGet]
        public RulesDto Get()
        {
            return _mapper.Map<RulesDto>(_rankingSettings);
        }
    }
}
