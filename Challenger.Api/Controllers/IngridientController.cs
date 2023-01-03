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
    public class IngridientController : Controller
    {
        private readonly IIngridientRepository _ingridientRepository;        
        private readonly IMapper _mapper;

        public IngridientController(
            IIngridientRepository ingridientRepository,
            IMapper mapper)
        {
            _ingridientRepository = ingridientRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IngridientDto> Get(long productId)
        {
            var all = await _ingridientRepository.Get(productId);
            return _mapper.Map<IngridientDto>(all);
        }

        [HttpPost]
        public async Task<IngridientDto> Add([FromBody] IngridientDto record)
        {
            var entity = _mapper.Map<Ingridient>(record);
            _ingridientRepository.Add(entity);
            await _ingridientRepository.SaveChanges();
            record = _mapper.Map<IngridientDto>(entity);

            return record;
        }

        [HttpPatch]
        public async Task<JsonResult> Update([FromBody] IngridientDto record)
        {
            await _ingridientRepository.Update(_mapper.Map<Ingridient>(record));
            await _ingridientRepository.SaveChanges();

            return Json(new { IsSuccess = true });
        }

        [HttpDelete]
        public async Task<JsonResult> Delete(long id)
        {
            var toDelete = await _ingridientRepository.Get(id);
            _ingridientRepository.Remove(toDelete);
            await _ingridientRepository.SaveChanges();

            return Json(new { IsSuccess = true });
        }
    }
}
