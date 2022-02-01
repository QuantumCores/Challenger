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

        public AccountController(
            IAccountService accountService,
            IJwtService jwtService)
        {
            _accountService = accountService;
            _jwtService = jwtService;
        }

        [HttpPost(ApiRoutes.Authentication.Register)]
        public async Task<JsonResult> Register([FromBody] RegisterModel registerModel)
        {
            var result = await _accountService.Register(registerModel);

            return Json(new { IsSuccess = result?.Succeeded, Errors = result?.Errors });
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
