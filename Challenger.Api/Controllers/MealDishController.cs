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
    public class MealDishController : Controller
    {
        private readonly IMealDishRepository _mealDishRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public MealDishController(
            IMealDishRepository mealDishRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _mealDishRepository = mealDishRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<MealDishDto[]> Get()
        {
            var all = await _mealDishRepository.GetAll();
            return _mapper.Map<MealDishDto[]>(all);
        }

        [HttpPost]
        public async Task<MealDishDto> Add([FromBody] MealDishDto record)
        {
            var entity = _mapper.Map<MealDish>(record);
            _mealDishRepository.Add(entity);
            await _mealDishRepository.SaveChanges();

            var recordResult = _mapper.Map<MealDishDto>(entity);
            recordResult.DishName = record.DishName;

            return record;
        }

        [HttpPatch]
        public async Task<JsonResult> Update([FromBody] MealDishDto record)
        {
            await _mealDishRepository.Update(_mapper.Map<MealDish>(record));
            await _mealDishRepository.SaveChanges();

            return Json(new { IsSuccess = true });
        }

        [HttpDelete]
        public async Task<JsonResult> Delete(long id)
        {
            var toDelete = await _mealDishRepository.Get(id);

            _mealDishRepository.Remove(toDelete);
            await _mealDishRepository.SaveChanges();

            return Json(new { IsSuccess = true });
        }
    }
}
