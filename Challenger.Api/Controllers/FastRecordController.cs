using AutoMapper;
using Challenger.Domain.Contracts.Repositories;
using Challenger.Domain.DbModels;
using Challenger.Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Challenger.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class FastRecordController : Controller
    {
        private readonly IFastRecordRepository _fastRecordRepository;
        private readonly IMapper _mapper;

        public FastRecordController(
            IFastRecordRepository fastRecordRepository,
            IMapper mapper)
        {
            _fastRecordRepository = fastRecordRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<FastRecordDto[]> Get()
        {
            var all = await _fastRecordRepository.GetAll();
            return _mapper.Map<FastRecordDto[]>(all);
        }

        [HttpPost]
        public async Task<FastRecordDto> Add([FromBody] FastRecordDto record)
        {
            var entity = _mapper.Map<FastRecord>(record);
            _fastRecordRepository.Add(entity);
            await _fastRecordRepository.SaveChanges();
            record = _mapper.Map<FastRecordDto>(entity);

            return record;
        }

        [HttpPatch]
        public async Task<JsonResult> Update([FromBody] FastRecordDto record)
        {
            await _fastRecordRepository.Update(_mapper.Map<FastRecord>(record));
            await _fastRecordRepository.SaveChanges();

            return Json(new { IsSuccess = true });
        }

        [HttpDelete]
        public async Task<JsonResult> Delete(long id)
        {
            var toDelete = await _fastRecordRepository.Get(id);

            _fastRecordRepository.Remove(toDelete);
            await _fastRecordRepository.SaveChanges();

            return Json(new { IsSuccess = true });
        }
    }
}
