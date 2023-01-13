using AutoMapper;
using Challenger.Domain.Contracts;
using Challenger.Domain.Contracts.Repositories;
using Challenger.Domain.DbModels;
using Challenger.Domain.Dtos;
using Challenger.Domain.FormulaParser;
using Challenger.Domain.FormulaParser.Contracts;
using Heimdal.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Challenger.Api.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("[controller]")]
    public class ChallengeController : Controller
    {
        private readonly ITokenProvider _tokenProvider;
        private readonly IChallengeRepository _challengeRepository;
        private readonly IChallengeService _challengeService;
        private readonly IMapper _mapper;

        public ChallengeController(
            ITokenProvider tokenProvider,
            IChallengeRepository challengeRepository,
            IChallengeService challengeService,
            IMapper mapper)
        {
            _tokenProvider = tokenProvider;
            _challengeRepository = challengeRepository;
            _challengeService = challengeService;

            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ChallengeDto[]> Get()
        {
            var all = await _challengeRepository.GetAllForUser(Guid.Parse(_tokenProvider.GetUserId()));
            return _mapper.Map<ChallengeDto[]>(all);
        }

        [HttpPost]
        public async Task<ChallengeDto> Add([FromBody] ChallengeDto record)
        {
            record.CreatorId = Guid.Parse(_tokenProvider.GetUserId());
            var entity = await _challengeService.CreateChallenge(record);
            record = _mapper.Map<ChallengeDto>(entity);

            return record;
        }

        [HttpPatch]
        public async Task<JsonResult> Update([FromBody] ChallengeDto record)
        {
            var userId = Guid.Parse(_tokenProvider.GetUserId());
            if (userId != record.CreatorId)
            {
                return Json(new { IsSuccess = false });
            }

            await _challengeRepository.Update(_mapper.Map<Challenge>(record));
            await _challengeRepository.SaveChanges();

            return Json(new { IsSuccess = true });
        }

        [HttpDelete]
        public async Task<JsonResult> Delete(long id)
        {
            var toDelete = await _challengeRepository.Get(id);
            var userId = Guid.Parse(_tokenProvider.GetUserId());
            if (userId != toDelete.CreatorId)
            {
                return Json(new { IsSuccess = false });
            }

            _challengeRepository.Remove(toDelete);
            await _challengeRepository.SaveChanges();

            return Json(new { IsSuccess = true });
        }

        [HttpPost("ValidateFormula")]
        public string ValidateFormula(string formula)
        {
            var rpn = RPNParser.Parse(formula);
            var expr = ExpressionBuilder.Build<FitRecord>(rpn.Output);
            var par = new FitRecord { Distance = 12 };
            var comp = expr.Compile();
            return expr.ToString() + " = " + comp(par).ToString();
        }
    }
}
