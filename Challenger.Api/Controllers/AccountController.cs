using Challenger.Api.Contracts.V1;
using Challenger.Domain.Account;
using Challenger.Domain.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Challenger.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IJwtService _jwtService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            IAccountService accountService,
            IJwtService jwtService,
            ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _jwtService = jwtService;
            _logger = logger;
        }

        [HttpPost(ApiRoutes.Authentication.Register)]
        public async Task<JsonResult> Register([FromBody] RegisterModel registerModel)
        {
            try
            {
                var result = await _accountService.Register(registerModel);

                return Json(new { IsSuccess = result?.Succeeded, Errors = result?.Errors });
            }
            catch (Exception ex)
            {
                return Json(new { IsSuccess = false, Errors = ex.ToString() });
            }
        }

        [HttpPost(ApiRoutes.Authentication.Login)]
        public async Task<IActionResult> Login([FromBody] LoginModel registerModel)
        {
            var result = await _accountService.Login(registerModel);

            if (result?.Succeeded ?? false)
            {
                var signingCredentials = _jwtService.GetSigningCredentials();
                var claims = _jwtService.GetClaims(registerModel.Email);
                var tokenOptions = _jwtService.GenerateTokenOptions(signingCredentials, claims);
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Json(new { IsSuccess = true, Token = token });
            }

            return Ok(new { IsSuccess = false, IsLockedOut = true });
        }
    }
}
