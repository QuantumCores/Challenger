using AutoMapper;
using Challenger.Api.Contracts.V1;
using Challenger.Domain.Account;
using Challenger.Domain.Contracts;
using Challenger.Domain.DbModels;
using Challenger.Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Challenger.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountController> _logger;

        public UserController(
            IUserRepository userRepository,
            IMapper mapper,
            ILogger<AccountController> logger)
        {
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
            var user = await _userRepository.GetByEmail(User.Identity.Name);
            return _mapper.Map<UserDto>(user);
        }
    }
}
