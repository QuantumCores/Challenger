using AutoMapper;
using Challenger.Domain.Contracts.Repositories;
using Challenger.Domain.DbModels;
using Challenger.Domain.Dtos;
using Heimdal.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Challenger.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class DiaryRecordController : Controller
    {
        private const int daysSpan = 7;
        private readonly ITokenProvider _tokenProvider;
        private readonly IDiaryRecordRepository _diaryRecordRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public DiaryRecordController(
            ITokenProvider tokenProvider,
            IDiaryRecordRepository diaryRecordRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _tokenProvider = tokenProvider;
            _diaryRecordRepository = diaryRecordRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<DiaryRecordDto[]> Get(DateTime startDate)
        {
            var userId = await _userRepository.GetIdByCorrelationId(User.Identity.Name);
            var all = await _diaryRecordRepository.GetAllForUser(userId, startDate, startDate.AddDays(daysSpan));
            return _mapper.Map<DiaryRecordDto[]>(all);
        }

        [HttpPost]
        public async Task<DiaryRecordDto> Add([FromBody] DiaryRecordDto record)
        {
            var entity = _mapper.Map<DiaryRecord>(record);
            entity.UserId = await _userRepository.GetIdByCorrelationId(User.Identity.Name);
            var exists = await _diaryRecordRepository.GetByDate(entity.UserId, record.DiaryDate.Date);

            if (exists != null)
            {
                throw new ArgumentException($"Diary record with date {record.DiaryDate.Date} already exists.");
            }

            _diaryRecordRepository.Add(entity);
            await _diaryRecordRepository.SaveChanges();
            record = _mapper.Map<DiaryRecordDto>(entity);

            return record;
        }

        [HttpPatch]
        public async Task<JsonResult> Update([FromBody] DiaryRecordDto record)
        {
            var userId = await _userRepository.GetIdByCorrelationId(User.Identity.Name);
            if (userId != record.UserId)
            {
                return Json(new { IsSuccess = false });
            }

            await _diaryRecordRepository.Update(_mapper.Map<DiaryRecord>(record));
            await _diaryRecordRepository.SaveChanges();

            return Json(new { IsSuccess = true });
        }

        [HttpDelete]
        public async Task<JsonResult> Delete(long id)
        {
            var toDelete = await _diaryRecordRepository.Get(id);
            var userId = await _userRepository.GetIdByCorrelationId(User.Identity.Name);
            if (userId != toDelete.UserId)
            {
                return Json(new { IsSuccess = false });
            }

            _diaryRecordRepository.Remove(toDelete);
            await _diaryRecordRepository.SaveChanges();

            return Json(new { IsSuccess = true });
        }
    }
}
