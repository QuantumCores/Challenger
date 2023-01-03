using AutoMapper;
using Challenger.Domain.Contracts.Repositories;
using Challenger.Domain.DbModels;
using Challenger.Domain.Dtos;
using Heimdal.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Challenger.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class FitRecordController : Controller
    {
        private readonly ITokenProvider _tokenProvider;
        private readonly IFitRecordRepository _fitRecordRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public FitRecordController(
            ITokenProvider tokenProvider,
            IFitRecordRepository fitRecordRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _tokenProvider = tokenProvider;
            _fitRecordRepository = fitRecordRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<FitRecordDto[]> Get()
        {
            var userId = await _userRepository.GetIdByCorrelationId(_tokenProvider.GetUserId());
            var all = await _fitRecordRepository.GetAllForUser(userId);
            return _mapper.Map<FitRecordDto[]>(all);
        }

        [HttpPost]
        public async Task<FitRecordDto> Add([FromBody] FitRecordDto record)
        {
            var entity = _mapper.Map<FitRecord>(record);
            entity.UserId = await _userRepository.GetIdByCorrelationId(_tokenProvider.GetUserId());
            _fitRecordRepository.Add(entity);
            await _fitRecordRepository.SaveChanges();
            record = _mapper.Map<FitRecordDto>(entity);

            return record;
        }

        [HttpPatch]
        public async Task<JsonResult> Update([FromBody] FitRecordDto record)
        {
            var userId = await _userRepository.GetIdByCorrelationId(_tokenProvider.GetUserId());
            if (userId != record.UserId)
            {
                return Json(new { IsSuccess = false });
            }

            await _fitRecordRepository.Update(_mapper.Map<FitRecord>(record));
            await _fitRecordRepository.SaveChanges();

            return Json(new { IsSuccess = true });
        }

        [HttpDelete]
        public async Task<JsonResult> Delete(long id)
        {
            var toDelete = await _fitRecordRepository.Get(id);
            var userId = await _userRepository.GetIdByCorrelationId(_tokenProvider.GetUserId());
            if (userId != toDelete.UserId)
            {
                return Json(new { IsSuccess = false });
            }

            _fitRecordRepository.Remove(toDelete);
            await _fitRecordRepository.SaveChanges();

            return Json(new { IsSuccess = true });
        }
    }
}
