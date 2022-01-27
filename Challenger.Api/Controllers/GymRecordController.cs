using AutoMapper;
using Challenger.Domain.Contracts;
using Challenger.Domain.DbModels;
using Challenger.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Challenger.Api.Controllers
{
    [ApiController]
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


        [HttpPost]
        public async Task<JsonResult> Add([FromBody] GymRecordDto record)
        {
            _gymRecordRepository.Add(_mapper.Map<GymRecord>(record));
            await _gymRecordRepository.SaveChanges();

            return Json(record);
        }

        [HttpPatch]
        public async Task<JsonResult> Update([FromBody] GymRecordDto record)
        {
            await _gymRecordRepository.Update(_mapper.Map<GymRecord>(record));
            await _gymRecordRepository.SaveChanges();

            return Json(record);
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
