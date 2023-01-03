using AutoMapper;
using Challenger.Api.Contracts.V1;
using Challenger.Domain.Contracts.Repositories;
using Challenger.Domain.DbModels;
using Challenger.Domain.Dtos;
using Heimdal.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Challenger.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly ITokenProvider _tokenProvider;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public UserController(
            ITokenProvider tokenProvider,
            IUserRepository userRepository,
            IMapper mapper,
            ILogger<UserController> logger)
        {
            _tokenProvider = tokenProvider;
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public Task<List<User>> GetAll()
        {
            return _userRepository.GetAll();
        }

        [Authorize]
        [HttpGet(ApiRoutes.User.Basic)]
        public async Task<UserDto> Get()
        {
            var user = await _userRepository.GetByCorrelationId(_tokenProvider.GetUserId());
            return _mapper.Map<UserDto>(user);
        }

        //[Authorize]
        [HttpPost(ApiRoutes.User.Create)]
        public async ValueTask<bool> CreateUser(string correlationId)
        {
            try
            {
                var user = await _userRepository.Add(new User { CorrelationId = Guid.Parse(correlationId) });
                await _userRepository.SaveChanges();
                return user?.Entity != null;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
