using AutoMapper;
using Challenger.Domain.Contracts;
using Challenger.Domain.DbModels;
using Challenger.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Challenger.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class MeasurementController : Controller
    {
        private readonly IMeasurementRepository _measurementRepository;
        private readonly IMapper _mapper;

        public MeasurementController(
            IMeasurementRepository measurementRepository,
            IMapper mapper)
        {
            _measurementRepository = measurementRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<MeasurementDto[]> Get()
        {
            var all = await _measurementRepository.GetAll();
            return _mapper.Map<MeasurementDto[]>(all);
        }

        [HttpPost]
        public async Task<MeasurementDto> Add([FromBody] MeasurementDto measurement)
        {
            var entity = _mapper.Map<Measurement>(measurement);
            entity.UserId = 1;
            _measurementRepository.Add(entity);
            await _measurementRepository.SaveChanges();

            return measurement;
        }

        [HttpPatch]
        public async Task<JsonResult> Update([FromBody] MeasurementDto measurement)
        {
            await _measurementRepository.Update(_mapper.Map<Measurement>(measurement));
            await _measurementRepository.SaveChanges();

            return Json(measurement);
        }

        [HttpDelete]
        public async Task<JsonResult> Delete(long id)
        {
            var toDelete = await _measurementRepository.Get(id);
            _measurementRepository.Remove(toDelete);
            await _measurementRepository.SaveChanges();

            return Json(new { IsSuccess = true });
        }
    }
}
