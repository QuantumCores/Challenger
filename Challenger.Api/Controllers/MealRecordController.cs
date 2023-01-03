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
    public class MealRecordController : Controller
    {
        private readonly IMealRecordRepository _mealRecordRepository;        
        private readonly IMapper _mapper;

        public MealRecordController(
            IMealRecordRepository mealRecordRepository,
            IMapper mapper)
        {
            _mealRecordRepository = mealRecordRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<MealRecordDto[]> Get()
        {   
            var all = await _mealRecordRepository.GetAll();
            return _mapper.Map<MealRecordDto[]>(all);
        }

        [HttpPost]
        public async Task<MealRecordDto> Add([FromBody] MealRecordDto record)
        {
            var entity = _mapper.Map<MealRecord>(record);            
            _mealRecordRepository.Add(entity);
            await _mealRecordRepository.SaveChanges();
            record = _mapper.Map<MealRecordDto>(entity);

            return record;
        }

        [HttpPatch]
        public async Task<JsonResult> Update([FromBody] MealRecordDto record)
        {
            await _mealRecordRepository.Update(_mapper.Map<MealRecord>(record));
            await _mealRecordRepository.SaveChanges();

            return Json(new { IsSuccess = true });
        }

        [HttpDelete]
        public async Task<JsonResult> Delete(long id)
        {
            var toDelete = await _mealRecordRepository.Get(id);            

            _mealRecordRepository.Remove(toDelete);
            await _mealRecordRepository.SaveChanges();

            return Json(new { IsSuccess = true });
        }
    }
}
