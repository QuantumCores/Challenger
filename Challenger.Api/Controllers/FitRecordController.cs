using AutoMapper;
using Challenger.Domain.Contracts;
using Challenger.Domain.DbModels;
using Challenger.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Challenger.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FitRecordController : Controller
    {
        private readonly IFitRecordRepository _fitRecordRepository;
        private readonly IMapper _mapper;

        public FitRecordController(
            IFitRecordRepository fitRecordRepository,
            IMapper mapper)
        {
            _fitRecordRepository = fitRecordRepository;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<JsonResult> Add([FromBody] FitRecordDto record)
        {
            _fitRecordRepository.Add(_mapper.Map<FitRecord>(record));
            await _fitRecordRepository.SaveChanges();

            return Json(record);
        }

        [HttpPatch]
        public async Task<JsonResult> Update([FromBody] FitRecordDto record)
        {
            await _fitRecordRepository.Update(_mapper.Map<FitRecord>(record));
            await _fitRecordRepository.SaveChanges();

            return Json(record);
        }

        [HttpDelete]
        public async Task<JsonResult> Delete(long id)
        {
            var toDelete = await _fitRecordRepository.Get(id);
            _fitRecordRepository.Remove(toDelete);
            await _fitRecordRepository.SaveChanges();

            return Json(new { IsSuccess = true });
        }
    }
}
