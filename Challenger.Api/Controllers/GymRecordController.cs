using AutoMapper;
using Challenger.Domain.Contracts.Repositories;
using Challenger.Domain.DbModels;
using Challenger.Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Challenger.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class GymRecordController : Controller
    {
        private readonly IGymRecordRepository _gymRecordRepository;
        private readonly IUserRepository _userRepository;
        private IMapper _mapper;

        public GymRecordController(
            IGymRecordRepository gymRecordRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _gymRecordRepository = gymRecordRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<GymRecordDto[]> Get()
        {
            var userId = await _userRepository.GetIdByEmail(User.Identity.Name);
            var all = await _gymRecordRepository.GetAllForUser(userId);
            return _mapper.Map<GymRecordDto[]>(all);
        }

        [HttpPost]
        public async Task<GymRecordDto> Add([FromBody] GymRecordDto record)
        {
            var entity = _mapper.Map<GymRecord>(record);
            entity.UserId = await _userRepository.GetIdByEmail(User.Identity.Name);
            _gymRecordRepository.Add(entity);
            await _gymRecordRepository.SaveChanges();
            record = _mapper.Map<GymRecordDto>(entity);

            return record;
        }

        [HttpPatch]
        public async Task<JsonResult> Update([FromBody] GymRecordDto record)
        {
            var userId = await _userRepository.GetIdByEmail(User.Identity.Name);
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
            var userId = await _userRepository.GetIdByEmail(User.Identity.Name);
            if (userId != toDelete.UserId)
            {
                return Json(new { IsSuccess = false });
            }

            _gymRecordRepository.Remove(toDelete);
            await _gymRecordRepository.SaveChanges();

            return Json(new { IsSuccess = true });
        }
    }
}
