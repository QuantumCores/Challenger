using Challenger.Identity.Migrations.IdentityServer.IdentityDb;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenger.Identity.Quickstart.User
{
    [ApiController]
    //[Authorize]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IdentityContext _identityContext;
        private readonly ILogger<UserController> _logger;        

        public UserController(         
            IdentityContext identityContext,
            ILogger<UserController> logger)
        {
            _identityContext = identityContext;
            _logger = logger;
        }

        [HttpGet("search")]
        public Task<List<IdentityUserSimple>> Search(string name, string userId)
        {
            return _identityContext.Users.Where(x => x.UserName.Contains(name) && x.EmailConfirmed && x.Id != userId)
                                         .Select(x => new IdentityUserSimple { Id = Guid.Parse(x.Id), UserName = x.UserName })
                                         .OrderBy(x => x.UserName)
                                         .Take(5)
                                         .ToListAsync();
        }
    }
}
