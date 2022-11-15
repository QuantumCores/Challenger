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
    public class DishController : Controller
    {
        private readonly ITokenProvider _tokenProvider;
        private readonly IDishRepository _dishRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public DishController(
            ITokenProvider tokenProvider,
            IDishRepository dishRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _tokenProvider = tokenProvider;
            _dishRepository = dishRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<DishDto[]> Get()
        {
            var userId = await _userRepository.GetIdByCorrelationId(_tokenProvider.GetUserId());
            var all = await _dishRepository.GetAllForUser(userId);
            return _mapper.Map<DishDto[]>(all);
        }

        [HttpGet("Find")]
        public async Task<DishDto[]> Find(string search)
        {
            var all = await _dishRepository.Find(search);
            return _mapper.Map<DishDto[]>(all);
        }

        [HttpPost]
        public async Task<DishDto> Add([FromBody] DishDto record)
        {
            var entity = _mapper.Map<Dish>(record);
            entity.UserId = await _userRepository.GetIdByCorrelationId(_tokenProvider.GetUserId());
            _dishRepository.Add(entity);
            await _dishRepository.SaveChanges();
            record = _mapper.Map<DishDto>(entity);

            return record;
        }

        [HttpPatch]
        public async Task<JsonResult> Update([FromBody] DishDto record)
        {
            var userId = await _userRepository.GetIdByCorrelationId(_tokenProvider.GetUserId());
            if (userId != record.UserId)
            {
                return Json(new { IsSuccess = false });
            }

            await _dishRepository.Update(_mapper.Map<Dish>(record));
            await _dishRepository.SaveChanges();

            return Json(new { IsSuccess = true });
        }

        [HttpDelete]
        public async Task<JsonResult> Delete(long id)
        {
            var toDelete = await _dishRepository.Get(id);
            var userId = await _userRepository.GetIdByCorrelationId(_tokenProvider.GetUserId());
            if (userId != toDelete.UserId)
            {
                return Json(new { IsSuccess = false });
            }

            _dishRepository.Remove(toDelete);
            await _dishRepository.SaveChanges();

            return Json(new { IsSuccess = true });
        }
    }
}
