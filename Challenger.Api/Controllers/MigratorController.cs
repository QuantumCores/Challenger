using Challenger.Api.Contracts.V1;
using Challenger.DbUp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Challenger.Api.Controllers
{
    [Authorize]
    public class MigratorController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string _appConnectionString;
        private readonly string _identityConnectionString;
        private readonly ScriptRunner _scriptRunner;

        public MigratorController(IConfiguration configuration)
        {
            _configuration = configuration;
            _appConnectionString = _configuration.GetConnectionString("AppConnection");
            _identityConnectionString = _configuration.GetConnectionString("IdentityConnection");

            _scriptRunner = new ScriptRunner(_identityConnectionString, _appConnectionString);
        }


        [HttpPost(ApiRoutes.Migrator.CreateIdentityDatabase)]
        public JsonResult CreateIdentityDatabase()
        {
            var result = _scriptRunner.RunIdentityDbScripts();

            var scripts = result.Scripts.Select(x => x.Name).ToList();
            return Json(new
            {
                result.Error.Message,
                result.ErrorScript,
                scripts,
                scripts.Count,
                result.Successful
            });
        }

        [HttpPost(ApiRoutes.Migrator.CreateAppDatabase)]
        public JsonResult CreateGameDatabase()
        {
            var result = _scriptRunner.RunGameDbScripts();

            var scripts = result.Scripts.Select(x => x.Name).ToList();
            return Json(new
            {
                result.Error.Message,
                result.ErrorScript,
                scripts,
                scripts.Count,
                result.Successful
            });
        }
    }

    public class ScriptResult
    {

        public string Error { get; set; }

    }
}
