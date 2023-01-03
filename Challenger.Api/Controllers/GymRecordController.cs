using AutoMapper;
using Challenger.Domain.Contracts.Repositories;
using Challenger.Domain.DbModels;
using Challenger.Domain.Dtos;
using Heimdal.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Challenger.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class GymRecordController : Controller
    {
        private readonly ITokenProvider _tokenProvider;
        private readonly IGymRecordRepository _gymRecordRepository;
        private readonly IUserRepository _userRepository;
        private IMapper _mapper;

        public GymRecordController(
            ITokenProvider tokenProvider,
            IGymRecordRepository gymRecordRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _tokenProvider = tokenProvider;
            _gymRecordRepository = gymRecordRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<GymRecordDto[]> Get()
        {
            var userId = await _userRepository.GetIdByCorrelationId(_tokenProvider.GetUserId());
            var all = await _gymRecordRepository.GetAllForUser(userId);
            return _mapper.Map<GymRecordDto[]>(all);
        }

        [HttpPost]
        public async Task<GymRecordDto> Add([FromBody] GymRecordDto record)
        {
            var entity = _mapper.Map<GymRecord>(record);
            entity.UserId = await _userRepository.GetIdByCorrelationId(_tokenProvider.GetUserId());
            _gymRecordRepository.Add(entity);
            await _gymRecordRepository.SaveChanges();
            record = _mapper.Map<GymRecordDto>(entity);

            return record;
        }

        [HttpPatch]
        public async Task<JsonResult> Update([FromBody] GymRecordDto record)
        {
            var userId = await _userRepository.GetIdByCorrelationId(_tokenProvider.GetUserId());
            if (userId != record.UserId)
            {
                return Json(new { IsSuccess = false });
            }

            await _gymRecordRepository.Update(_mapper.Map<GymRecord>(record));
            await _gymRecordRepository.SaveChanges();

            return Json(new { IsSuccess = true });
        }

        [HttpDelete]
        public async Task<JsonResult> Delete(long id)
        {
            var toDelete = await _gymRecordRepository.Get(id);
            var userId = await _userRepository.GetIdByCorrelationId(_tokenProvider.GetUserId());
            if (userId != toDelete.UserId)
            {
                return Json(new { IsSuccess = false });
            }

            _gymRecordRepository.Remove(toDelete);
            await _gymRecordRepository.SaveChanges();

            return Json(new { IsSuccess = true });
        }

        [HttpGet("dynamic")]
        public async Task<List<GymRecord>> Dynamic()
        {
            var result = await _gymRecordRepository.Dynamic();

            return result;
        }
    }
}
