using Challenger.Api.Contracts.V1;
using Challenger.Domain.Account;
using Challenger.Domain.Contracts.Account;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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

        
        [HttpGet("IdentityTest")]
        public async Task<string> IdentityTest()
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return null;
            }

            // request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "client",
                ClientSecret = "challenger.secret",
                Scope = "challenger"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return null;
            }

            // call api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync("https://localhost:5002/Account");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
                return content;
            }

            return null;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}
