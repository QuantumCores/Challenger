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
    public class MeasurementController : Controller
    {
        private readonly ITokenProvider _tokenProvider;
        private readonly IMeasurementRepository _measurementRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public MeasurementController(
            ITokenProvider tokenProvider,
            IMeasurementRepository measurementRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _tokenProvider = tokenProvider;
            _measurementRepository = measurementRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<MeasurementDto[]> Get()
        {
            var userId = await _userRepository.GetIdByCorrelationId(_tokenProvider.GetUserId());
            var all = await _measurementRepository.GetAllForUser(userId);
            return _mapper.Map<MeasurementDto[]>(all);
        }

        [HttpPost]
        public async Task<MeasurementDto> Add([FromBody] MeasurementDto measurement)
        {   
            var entity = _mapper.Map<Measurement>(measurement);
            entity.UserId = await _userRepository.GetIdByCorrelationId(_tokenProvider.GetUserId());
            _measurementRepository.Add(entity);
            await _measurementRepository.SaveChanges();
            measurement = _mapper.Map<MeasurementDto>(entity);

            return measurement;
        }

        [HttpPatch]
        public async Task<JsonResult> Update([FromBody] MeasurementDto measurement)
        {
            var userId = await _userRepository.GetIdByCorrelationId(_tokenProvider.GetUserId());
            if (userId != measurement.UserId)
            {
                return Json(new { IsSuccess = false });
            }

            await _measurementRepository.Update(_mapper.Map<Measurement>(measurement));
            await _measurementRepository.SaveChanges();

            return Json(new { IsSuccess = true });
        }

        [HttpDelete]
        public async Task<JsonResult> Delete(long id)
        {
            var toDelete = await _measurementRepository.Get(id);
            var userId = await _userRepository.GetIdByCorrelationId(_tokenProvider.GetUserId());
            if (userId != toDelete.UserId)
            {
                return Json(new { IsSuccess = false });
            }

            _measurementRepository.Remove(toDelete);
            await _measurementRepository.SaveChanges();

            return Json(new { IsSuccess = true });
        }
    }
}
