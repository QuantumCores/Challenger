using AutoMapper;
using Challenger.Domain.ChallengeService;
using Challenger.Domain.Contracts.Repositories;
using Challenger.Domain.Contracts.Services;
using Challenger.Domain.DbModels;
using Challenger.Domain.Dtos;
using Challenger.Domain.FormulaService;
using Heimdal.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Challenger.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ChallengeController : Controller
    {
        private readonly ITokenProvider _tokenProvider;
        private readonly IChallengeRepository _challengeRepository;
        private readonly IUserChallengeRepository _userChallengeRepository;
        private readonly IChallengeService _challengeService;
        private readonly IFormulaService _formulaService;
        private readonly ChallengeSettings _settings;
        private readonly DefaultForumulaSetting[] _defaultForumulaSettings;
        private readonly IMapper _mapper;

        public ChallengeController(
            ITokenProvider tokenProvider,
            IChallengeRepository challengeRepository,
            IUserChallengeRepository userChallengeRepository,
            IChallengeService challengeService,
            IFormulaService formulaService,
            ChallengeSettings settings,
            DefaultForumulaSetting[] defaultForumulaSettings,
            IMapper mapper)
        {
            _tokenProvider = tokenProvider;
            _challengeRepository = challengeRepository;
            _userChallengeRepository = userChallengeRepository;
            _challengeService = challengeService;
            _formulaService = formulaService;
            _settings = settings;
            _defaultForumulaSettings = defaultForumulaSettings;
            _mapper = mapper;
        }

        [HttpGet("records")]
        public Task<ChallengeDisplayDto[]> GetRecords()
        {
            return _challengeService.GetForUser(Guid.Parse(_tokenProvider.GetUserId()));
        }

        [HttpGet]
        public Task<ChallengeDto> Get(long id)
        {
            return _challengeService.GetChallenge(id);
        }

        [HttpGet("Search")]
        public Task<ChallengeDisplayDto[]> Search(string name)
        {
            return _challengeService.GetByName(Guid.Parse(_tokenProvider.GetUserId()), name);
        }

        [HttpGet("Settings")]
        public ChallengeSettings Settings()
        {
            return _settings;
        }

        [HttpGet("DefaultFormulas")]
        public DefaultForumulaSetting[] GetDefaultFormulas()
        {
            return _defaultForumulaSettings;
        }

        [HttpPatch("Join")]
        public Task<bool> JoinChallenge(int challengeId)
        {
            return _challengeService.JoinChallenge(Guid.Parse(_tokenProvider.GetUserId()), challengeId);
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
        public Task<ChallengeDto> Update([FromBody] ChallengeDto record)
        {
            var userId = Guid.Parse(_tokenProvider.GetUserId());
            record.CreatorId = userId;
            return _challengeService.UpdateChallenge(record);
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
        public FormulaValidationResult ValidateFormula(FormulaValidationDto challengeFormulaStore)
        {
            var result = new FormulaValidationResult();
            var fit = _formulaService.ValidateFitFormula(challengeFormulaStore.FitFormula);
            var gym = _formulaService.ValidateGymFormula(challengeFormulaStore.GymFormula);
            var mes = _formulaService.ValidateMeasurementFormula(challengeFormulaStore.MeasurementFormula);

            result.IsValid = fit.IsValid && gym.IsValid && mes.IsValid;
            result.FitValidationMesage = fit.FitValidationMesage;
            result.GymValidationMesage = gym.GymValidationMesage;
            result.MeasurementValidationMesage = mes.MeasurementValidationMesage;

            return result;
        }
    }
}
