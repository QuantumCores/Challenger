using AutoMapper;
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
    //[Authorize]
    [Route("[controller]")]
    public class ChallengeController : Controller
    {
        private readonly ITokenProvider _tokenProvider;
        private readonly IChallengeRepository _challengeRepository;
        private readonly IChallengeService _challengeService;
        private readonly IFormulaService _formulaService;
        private readonly DefaultForumulaSetting[] _defaultForumulaSettings;
        private readonly IMapper _mapper;

        public ChallengeController(
            ITokenProvider tokenProvider,
            IChallengeRepository challengeRepository,
            IChallengeService challengeService,
            IFormulaService formulaService,
            DefaultForumulaSetting[] defaultForumulaSettings,
            IMapper mapper)
        {
            _tokenProvider = tokenProvider;
            _challengeRepository = challengeRepository;
            _challengeService = challengeService;
            _formulaService = formulaService;
            _defaultForumulaSettings = defaultForumulaSettings;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ChallengeDto[]> Get()
        {
            var all = await _challengeRepository.GetAllForUser(Guid.Parse(_tokenProvider.GetUserId()));
            return _mapper.Map<ChallengeDto[]>(all);
        }

        [HttpGet("DefaultFormulas")]
        public DefaultForumulaSetting[] GetDefaultFormulas()
        {   
            return _defaultForumulaSettings;
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
        public FormulaValidationResult ValidateFormula(FormulaValidationDto challengeFormulaStore)
        {
            var result = new FormulaValidationResult();
            var fit = _formulaService.ValidateFitFormula(challengeFormulaStore.FitFormula);
            var gym = _formulaService.ValidateGymFormula(challengeFormulaStore.GymFormula);
            var mes = _formulaService.ValidateMeasurementFormula(challengeFormulaStore.MeasurementFormula);

            result.IsValid = fit.IsValid && gym.IsValid && mes.IsValid;
            result.FitValidationMesage = fit.FitValidationMesage;
            result.GymValidationMesage = gym.FitValidationMesage;
            result.MeasurementValidationMesage = mes.FitValidationMesage;

            return result;
        }
    }
}
