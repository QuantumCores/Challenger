using Microsoft.AspNetCore.Mvc;

namespace Challenger.Api.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
