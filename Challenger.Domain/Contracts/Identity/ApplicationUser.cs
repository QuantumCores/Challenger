using System;

namespace Challenger.Domain.Contracts.Identity
{
    public class ApplicationUser
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public Guid Id { get; set; }

        public string Avatar { get; set; }
    }
}
