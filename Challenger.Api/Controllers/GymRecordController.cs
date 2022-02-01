using AutoMapper;
using Challenger.Domain.Contracts;
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
        private IMapper _mapper;

        public GymRecordController(
            IGymRecordRepository gymRecordRepository,
            IMapper mapper)
        {
            _gymRecordRepository = gymRecordRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<GymRecordDto[]> Get()
        {
            var all = await _gymRecordRepository.GetAll();
            return _mapper.Map<GymRecordDto[]>(all);
        }

        [HttpPost]
        public async Task<GymRecordDto> Add([FromBody] GymRecordDto record)
        {
            var entity = _mapper.Map<GymRecord>(record);
            entity.UserId = 1;
            _gymRecordRepository.Add(entity);
            await _gymRecordRepository.SaveChanges();

            return record;
        }

        [HttpPatch]
        public async Task<JsonResult> Update([FromBody] GymRecordDto record)
        {
            await _gymRecordRepository.Update(_mapper.Map<GymRecord>(record));
            await _gymRecordRepository.SaveChanges();

            return Json(new { IsSuccess = true });
        }

        [HttpDelete]
        public async Task<JsonResult> Delete(long id)
        {
            var toDelete = await _gymRecordRepository.Get(id);
            _gymRecordRepository.Remove(toDelete);
            await _gymRecordRepository.SaveChanges();

            return Json(new { IsSuccess = true });
        }
    }
}
