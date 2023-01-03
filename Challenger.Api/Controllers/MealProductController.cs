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
    public class MealProductController : Controller
    {
        private readonly IMealProductRepository _mealProductRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public MealProductController(
            IMealProductRepository mealProductRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _mealProductRepository = mealProductRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<MealProductDto[]> Get()
        {
            var all = await _mealProductRepository.GetAll();
            return _mapper.Map<MealProductDto[]>(all);
        }

        [HttpPost]
        public async Task<MealProductDto> Add([FromBody] MealProductDto record)
        {
            var entity = _mapper.Map<MealProduct>(record);
            _mealProductRepository.Add(entity);
            await _mealProductRepository.SaveChanges();

            var recordResult = _mapper.Map<MealProductDto>(entity);
            recordResult.ProductName = record.ProductName;

            return record;
        }

        [HttpPatch]
        public async Task<JsonResult> Update([FromBody] MealProductDto record)
        {
            await _mealProductRepository.Update(_mapper.Map<MealProduct>(record));
            await _mealProductRepository.SaveChanges();

            return Json(new { IsSuccess = true });
        }

        [HttpDelete]
        public async Task<JsonResult> Delete(long id)
        {
            var toDelete = await _mealProductRepository.Get(id);

            _mealProductRepository.Remove(toDelete);
            await _mealProductRepository.SaveChanges();

            return Json(new { IsSuccess = true });
        }
    }
}
