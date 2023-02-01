using AutoMapper;
using Challenger.Api.Contracts.V1;
using Challenger.Domain.Contracts.Identity;
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
    //[Authorize]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly ITokenProvider _tokenProvider;
        private readonly IUserRepository _userRepository;
        private readonly IIdentityApi _identityApi;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public UserController(
            ITokenProvider tokenProvider,
            IUserRepository userRepository,
            IIdentityApi identityApi,
            IMapper mapper,
            ILogger<UserController> logger)
        {
            _tokenProvider = tokenProvider;
            _userRepository = userRepository;
            _identityApi = identityApi;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public Task<List<User>> GetAll()
        {
            return _userRepository.GetAll();
        }

        [HttpGet(ApiRoutes.User.Search)]
        public Task<List<ApplicationUser>> Search(string name)
        {
            var userId = Guid.Parse(_tokenProvider.GetUserId());
            return _identityApi.SearchUsersByName(name, userId);
        }

        [HttpPut("Update")]
        public async Task<UserDto> Update(UserDto update)
        {
            var userId = Guid.Parse(_tokenProvider.GetUserId());
            if (update.CorrelationId != userId)
            {
                throw new InvalidOperationException();
            }

            var entity = await _userRepository.GetByCorrelationId(userId);
            entity.Height = update.Height;
            entity.DateOfBirth = update.DateOfBirth;
            entity.Sex = update.Sex;
            await _userRepository.Update(entity);
            await _userRepository.SaveChanges();
            await _identityApi.UpdateUser(update.Avatar, userId);
            return update;
        }

        [Authorize]
        [HttpGet(ApiRoutes.User.Basic)]
        public async Task<UserDto> Get()
        {
            var userGuid = Guid.Parse(_tokenProvider.GetUserId());
            var user = await _userRepository.GetByCorrelationId(userGuid);
            var identity = await _identityApi.GetUsers(new Guid[] { userGuid });            
            var mapped = _mapper.Map<UserDto>(user);
            mapped.UserName = identity[0].UserName;
            mapped.Avatar = identity[0].Avatar;
            return mapped;
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
