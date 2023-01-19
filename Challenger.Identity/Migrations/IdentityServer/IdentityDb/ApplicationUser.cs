using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Challenger.Identity.Migrations.IdentityServer.IdentityDb
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(7)]
        public string Avatar { get; set; }
    }
}
