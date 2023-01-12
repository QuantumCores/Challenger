using System;

namespace Challenger.Domain.Contracts.Identity
{
    public class IdentityUser
    {
        public string UserName { get; set; }

        public Guid Id { get; set; }
    }
}
