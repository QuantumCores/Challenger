using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Challenger.Identity.Quickstart.Register
{
    [AllowAnonymous]
    public class ConfirmEmailViewModel
    {
        [TempData]
        public string StatusMessage { get; set; }
    }
}
