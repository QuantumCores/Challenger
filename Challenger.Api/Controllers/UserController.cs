using Challenger.Domain.Account;
using Challenger.Domain.Contracts;
using Challenger.Domain.DbModels;
using Microsoft.AspNetCore.Mvc;

namespace Challenger.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly ILogger<AccountController> _logger;

        public UserController(
            IUserRepository userRepository,
            IJwtService jwtService,
            ILogger<AccountController> logger)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _logger = logger;
        }

        [HttpGet]
        public Task<List<User>> GetAll()
        {
            return _userRepository.GetAll();
        }
    }
}
